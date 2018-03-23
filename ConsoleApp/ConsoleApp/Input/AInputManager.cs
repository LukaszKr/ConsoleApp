using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class AInputManager
	{
		public readonly KeyboardDevice Keyboard = new KeyboardDevice();
		public readonly MouseDevice Mouse = new MouseDevice();

		protected List<InputLayer> m_ActiveLayers = new List<InputLayer>();
		public List<LayerDefinition> LayerDefinitions = new List<LayerDefinition>();

		public abstract Type EnumIDType { get; }
		public double DeltaTime { get; private set; }

		private InputRecord[] m_Buffer = new InputRecord[32];

		public void Update(double deltaTime)
		{
			DeltaTime = deltaTime;
			UpdateDevices();
			UpdateActiveLayers();
		}

		private void UpdateDevices()
		{
			Keyboard.UpdateState();

			if(ConsoleHelper.ReadAvail() > 0)
			{
				uint size = ConsoleHelper.ReadInput(m_Buffer);
				for(int x = 0; x < size; ++x)
				{
					InputRecord record = m_Buffer[x];
					switch(record.EventType)
					{ 
						case EInputEvent.MouseEvent:
							Mouse.ProcessRecord(record);
							break;
						case EInputEvent.KeyEvent:
							Keyboard.ProcessRecord(record);
							break;
						case EInputEvent.WindowBufferSizeEvent:
							break;
					}
				}
			}

		}

		#region Shortcut
		public EButtonState Get(ConsoleKey key)
		{
			return Keyboard.Get(key);
		}

		public EButtonState Get(EInputModifier key)
		{
			return Keyboard.Get(key);
		}
		#endregion

		#region Layers
		private void UpdateActiveLayers()
		{
			bool isActive = true;
			int count = m_ActiveLayers.Count - 1;
			for(int x = count; x >= 0; --x)
			{
				InputLayer layer = m_ActiveLayers[x];
				layer.IsActive = isActive;
				if(isActive)
				{
					layer.Receiver.UpdateInput(this);
				}
				if(layer.Definition.Block)
				{
					isActive = false;
				}
			}
		}

		public List<InputLayer> GetActiveLayers()
		{
			return m_ActiveLayers;
		}

		protected LayerDefinition GetLayerDefinition(int layerID)
		{
			int count = LayerDefinitions.Count;
			for(int x = 0; x != count; ++x)
			{
				LayerDefinition definition = LayerDefinitions[x];
				if(definition.ID == layerID)
				{
					return definition;
				}
			}
			return null;
		}
		#endregion

		#region Receiver
		public bool PopReceiver(IInputReceiver receiver)
		{
			int index = IndexOfReceiver(receiver);
			if(index == -1)
			{
				return false;
			}
			m_ActiveLayers.RemoveAt(index);
			return true;
		}

		protected int IndexOfReceiver(IInputReceiver receiver)
		{
			int count = m_ActiveLayers.Count;

			for(int x = 0; x != count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Receiver == receiver)
				{
					return x;
				}
			}
			return -1;
		}

		protected void PushReceiver(IInputReceiver receiver, int layerID)
		{
			if(IndexOfReceiver(receiver) >= 0)
			{
				throw new ArgumentException("Receiver is already active");
			}

			LayerDefinition definition = GetLayerDefinition(layerID);
			if(definition == null)
			{
				throw new ArgumentNullException(string.Format("Layer with ID: {0} was not found.", layerID));
			}

			InputLayer newLayer = new InputLayer(receiver, definition);

			int count = m_ActiveLayers.Count;
			for(int x = 0; x != count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Definition.Priority > definition.Priority)
				{
					m_ActiveLayers.Insert(x, newLayer);
					return;
				}
			}
			m_ActiveLayers.Add(newLayer);
		}
		#endregion
	}
}
