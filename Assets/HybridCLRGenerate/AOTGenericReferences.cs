using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"JKFrame.dll",
		"System.Core.dll",
		"System.dll",
		"Unity.Netcode.Runtime.dll",
		"Unity.ResourceManager.dll",
		"UnityEngine.CoreModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// DelegateList<float>
	// JKFrame.EventModule.MultipleParameterEventInfo<object>
	// JKFrame.ResSystem.<>c__DisplayClass21_0<object>
	// JKFrame.SingletonMono<object>
	// System.Action<GameSceneLaunchEvent>
	// System.Action<InitClientAOIEvent>
	// System.Action<LocalPlayerEvent>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,object>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Action<UnityEngine.Vector2Int>
	// System.Action<UnityEngine.Vector3,UnityEngine.Quaternion>
	// System.Action<UpdateClientAOIEvent>
	// System.Action<float>
	// System.Action<object>
	// System.Action<ulong,object>
	// System.Action<ulong>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2Int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2Int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<UnityEngine.Vector2Int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.LinkedList.Enumerator<object>
	// System.Collections.Generic.LinkedList<object>
	// System.Collections.Generic.LinkedListNode<object>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<UnityEngine.Vector2Int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.Queue.Enumerator<int>
	// System.Collections.Generic.Queue<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<UnityEngine.Vector2Int>
	// System.Comparison<object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Predicate<UnityEngine.Vector2Int>
	// System.Predicate<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskCompletionSource<object>
	// Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerReader>
	// Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerWriter>
	// Unity.Netcode.BufferSerializer<object>
	// Unity.Netcode.FallbackSerializer<int>
	// Unity.Netcode.FallbackSerializer<object>
	// Unity.Netcode.INetworkVariableSerializer<int>
	// Unity.Netcode.INetworkVariableSerializer<object>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<int>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<object>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<int>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<object>
	// Unity.Netcode.NetworkVariable<int>
	// Unity.Netcode.NetworkVariable<object>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<int>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<object>
	// Unity.Netcode.NetworkVariableSerialization<int>
	// Unity.Netcode.NetworkVariableSerialization<object>
	// Unity.Netcode.UnmanagedTypeSerializer<int>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>
	// UnityEngine.ResourceManagement.Util.GlobalLinkedListNodeCache<object>
	// UnityEngine.ResourceManagement.Util.LinkedListNodeCache<object>
	// }}

	public void RefMethods()
	{
		// System.Void JKFrame.EventModule.AddEventListener<object>(string,object)
		// System.Void JKFrame.EventModule.AddMultipleParameterEventInfo<object>(string,object)
		// System.Void JKFrame.EventModule.EventTrigger<InitClientAOIEvent>(string,InitClientAOIEvent)
		// System.Void JKFrame.EventModule.EventTrigger<LocalPlayerEvent>(string,LocalPlayerEvent)
		// System.Void JKFrame.EventModule.EventTrigger<UpdateClientAOIEvent>(string,UpdateClientAOIEvent)
		// System.Void JKFrame.EventSystem.AddEventListener<GameSceneLaunchEvent>(string,System.Action<GameSceneLaunchEvent>)
		// System.Void JKFrame.EventSystem.AddEventListener<LocalPlayerEvent>(string,System.Action<LocalPlayerEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(System.Action<GameSceneLaunchEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<LocalPlayerEvent>(System.Action<LocalPlayerEvent>)
		// System.Void JKFrame.EventSystem.EventTrigger<InitClientAOIEvent>(string,InitClientAOIEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<LocalPlayerEvent>(string,LocalPlayerEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<UpdateClientAOIEvent>(string,UpdateClientAOIEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<InitClientAOIEvent>(InitClientAOIEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<LocalPlayerEvent>(LocalPlayerEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<UpdateClientAOIEvent>(UpdateClientAOIEvent)
		// object JKFrame.ResSystem.InstantiateGameObject<object>(string,UnityEngine.Transform,string,bool)
		// System.Void JKFrame.ResSystem.InstantiateGameObjectAsync<object>(string,System.Action<object>,UnityEngine.Transform,string,bool)
		// object System.Activator.CreateInstance<object>()
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<int>(int&)
		// System.Void Unity.Netcode.BufferSerializer<object>.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializable<object>(object&)
		// System.Void Unity.Netcode.FastBufferReader.ReadUnmanagedSafe<int>(int&)
		// System.Void Unity.Netcode.FastBufferReader.ReadUnmanagedSafe<object>(object&)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<object>(object&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanagedSafe<int>(int&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanagedSafe<object>(object&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.INetworkSerializable.NetworkSerialize<Unity.Netcode.BufferSerializerReader>(Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerReader>)
		// System.Void Unity.Netcode.INetworkSerializable.NetworkSerialize<Unity.Netcode.BufferSerializerWriter>(Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerWriter>)
		// System.Void Unity.Netcode.IReaderWriter.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// bool Unity.Netcode.NetworkVariableSerialization<int>.ValueEquals<int>(int&,int&)
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeEqualityChecker_UnmanagedValueEquals<int>()
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeSerializer_UnmanagedByMemcpy<int>()
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.Object.Instantiate<object>(object)
	}
}