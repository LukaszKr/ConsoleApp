using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class InputManager<ELayerID>: AInputManager
	{
		public override Type EnumIDType { get { return typeof(ELayerID); } }

		public void PushReceiver(IInputReceiver receiver, ELayerID layerID)
		{
			PushReceiver(receiver, IDToInt(layerID));
		}

		protected virtual int IDToInt(ELayerID id)
		{
			return id.GetHashCode();
		}

		public void AddLayer(ELayerID id, int priority, bool block)
		{
			LayerDefinitions.Add(new LayerDefinition(IDToInt(id), priority, block));
		}
	}
}
