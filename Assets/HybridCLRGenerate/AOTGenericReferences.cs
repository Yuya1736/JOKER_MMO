using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"JKFrame.dll",
		"System.Core.dll",
		"System.dll",
		"Unity.Addressables.dll",
		"Unity.Collections.dll",
		"Unity.Netcode.Runtime.dll",
		"Unity.ResourceManager.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// DelegateList<float>
	// JKFrame.EventModule.MultipleParameterEventInfo<object>
	// JKFrame.ResSystem.<>c__DisplayClass21_0<object>
	// JKFrame.SingletonMono<object>
	// LocalizationConfigBase<int>
	// System.Action<CheckUIInputBlockerEvent>
	// System.Action<CrafterItemConfig>
	// System.Action<GameSceneLaunchEvent>
	// System.Action<InitClientAOIEvent>
	// System.Action<LocalPlayerEvent>
	// System.Action<MerchantItemConfig>
	// System.Action<TerrainAudioVolumeChangeEvent>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,object>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Action<UnityEngine.Vector2Int>
	// System.Action<UnityEngine.Vector3,UnityEngine.Quaternion>
	// System.Action<UpdateClientAOIEvent>
	// System.Action<float>
	// System.Action<int>
	// System.Action<object,object>
	// System.Action<object>
	// System.Action<ulong,object>
	// System.Collections.Generic.ArraySortHelper<CrafterItemConfig>
	// System.Collections.Generic.ArraySortHelper<MerchantItemConfig>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2Int>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<CrafterItemConfig>
	// System.Collections.Generic.Comparer<MerchantItemConfig>
	// System.Collections.Generic.Comparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.Enumerator<byte,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<byte,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<byte,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<byte,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<byte,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary<byte,object>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.ICollection<CrafterItemConfig>
	// System.Collections.Generic.ICollection<MerchantItemConfig>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<byte,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2Int>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<CrafterItemConfig>
	// System.Collections.Generic.IComparer<MerchantItemConfig>
	// System.Collections.Generic.IComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<CrafterItemConfig>
	// System.Collections.Generic.IEnumerable<MerchantItemConfig>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<byte,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<CrafterItemConfig>
	// System.Collections.Generic.IEnumerator<MerchantItemConfig>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<byte,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEqualityComparer<byte>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<CrafterItemConfig>
	// System.Collections.Generic.IList<MerchantItemConfig>
	// System.Collections.Generic.IList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IList<UnityEngine.Vector2Int>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.IReadOnlyCollection<object>
	// System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.KeyValuePair<byte,object>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.LinkedList.Enumerator<object>
	// System.Collections.Generic.LinkedList<object>
	// System.Collections.Generic.LinkedListNode<object>
	// System.Collections.Generic.List.Enumerator<CrafterItemConfig>
	// System.Collections.Generic.List.Enumerator<MerchantItemConfig>
	// System.Collections.Generic.List.Enumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<CrafterItemConfig>
	// System.Collections.Generic.List<MerchantItemConfig>
	// System.Collections.Generic.List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.List<UnityEngine.Vector2Int>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<CrafterItemConfig>
	// System.Collections.Generic.ObjectComparer<MerchantItemConfig>
	// System.Collections.Generic.ObjectComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.Queue.Enumerator<int>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<int>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<CrafterItemConfig>
	// System.Collections.ObjectModel.ReadOnlyCollection<MerchantItemConfig>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<CrafterItemConfig>
	// System.Comparison<MerchantItemConfig>
	// System.Comparison<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Comparison<UnityEngine.Vector2Int>
	// System.Comparison<int>
	// System.Comparison<object>
	// System.Func<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Func<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Func<object,UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Func<object,object>
	// System.Func<object>
	// System.IEquatable<Unity.Collections.FixedString32Bytes>
	// System.Predicate<CrafterItemConfig>
	// System.Predicate<MerchantItemConfig>
	// System.Predicate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Predicate<UnityEngine.Vector2Int>
	// System.Predicate<int>
	// System.Predicate<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.Task<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskCompletionSource<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.TaskCompletionSource<object>
	// Unity.Collections.IIndexable<byte>
	// Unity.Collections.INativeList<byte>
	// Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerReader>
	// Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerWriter>
	// Unity.Netcode.BufferSerializer<object>
	// Unity.Netcode.FallbackSerializer<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.FallbackSerializer<int>
	// Unity.Netcode.FallbackSerializer<object>
	// Unity.Netcode.FixedStringSerializer<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.INetworkVariableSerializer<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.INetworkVariableSerializer<int>
	// Unity.Netcode.INetworkVariableSerializer<object>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<int>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<object>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<int>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<object>
	// Unity.Netcode.NetworkVariable<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.NetworkVariable<int>
	// Unity.Netcode.NetworkVariable<object>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<int>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<object>
	// Unity.Netcode.NetworkVariableSerialization<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.NetworkVariableSerialization<int>
	// Unity.Netcode.NetworkVariableSerialization<object>
	// Unity.Netcode.UnmanagedTypeSerializer<int>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<object>
	// Unity.Netcode.UserNetworkVariableSerialization<Unity.Collections.FixedString32Bytes>
	// Unity.Netcode.UserNetworkVariableSerialization<object>
	// UnityEngine.AddressableAssets.AddressablesImpl.<>c__DisplayClass79_0<object>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Events.UnityEvent<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>
	// UnityEngine.ResourceManagement.ChainOperationTypelessDepedency<object>
	// UnityEngine.ResourceManagement.ResourceManager.CompletedOperation<object>
	// UnityEngine.ResourceManagement.Util.GlobalLinkedListNodeCache<object>
	// UnityEngine.ResourceManagement.Util.LinkedListNodeCache<object>
	// }}

	public void RefMethods()
	{
		// System.Void JKFrame.EventModule.AddEventListener<object>(string,object)
		// System.Void JKFrame.EventModule.AddMultipleParameterEventInfo<object>(string,object)
		// System.Void JKFrame.EventModule.EventTrigger<CheckUIInputBlockerEvent>(string,CheckUIInputBlockerEvent)
		// System.Void JKFrame.EventModule.EventTrigger<InitClientAOIEvent>(string,InitClientAOIEvent)
		// System.Void JKFrame.EventModule.EventTrigger<LocalPlayerEvent>(string,LocalPlayerEvent)
		// System.Void JKFrame.EventModule.EventTrigger<TerrainAudioVolumeChangeEvent>(string,TerrainAudioVolumeChangeEvent)
		// System.Void JKFrame.EventModule.EventTrigger<UpdateClientAOIEvent>(string,UpdateClientAOIEvent)
		// System.Void JKFrame.EventModule.RemoveEventListener<object>(string,object)
		// System.Void JKFrame.EventSystem.AddEventListener<CheckUIInputBlockerEvent>(string,System.Action<CheckUIInputBlockerEvent>)
		// System.Void JKFrame.EventSystem.AddEventListener<GameSceneLaunchEvent>(string,System.Action<GameSceneLaunchEvent>)
		// System.Void JKFrame.EventSystem.AddEventListener<LocalPlayerEvent>(string,System.Action<LocalPlayerEvent>)
		// System.Void JKFrame.EventSystem.AddEventListener<TerrainAudioVolumeChangeEvent>(string,System.Action<TerrainAudioVolumeChangeEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<CheckUIInputBlockerEvent>(System.Action<CheckUIInputBlockerEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(System.Action<GameSceneLaunchEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<LocalPlayerEvent>(System.Action<LocalPlayerEvent>)
		// System.Void JKFrame.EventSystem.AddTypeEventListener<TerrainAudioVolumeChangeEvent>(System.Action<TerrainAudioVolumeChangeEvent>)
		// System.Void JKFrame.EventSystem.EventTrigger<CheckUIInputBlockerEvent>(string,CheckUIInputBlockerEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<InitClientAOIEvent>(string,InitClientAOIEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<LocalPlayerEvent>(string,LocalPlayerEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<TerrainAudioVolumeChangeEvent>(string,TerrainAudioVolumeChangeEvent)
		// System.Void JKFrame.EventSystem.EventTrigger<UpdateClientAOIEvent>(string,UpdateClientAOIEvent)
		// System.Void JKFrame.EventSystem.RemoveTypeEventListener<LocalPlayerEvent>(System.Action<LocalPlayerEvent>)
		// System.Void JKFrame.EventSystem.RemoveTypeEventListener<TerrainAudioVolumeChangeEvent>(System.Action<TerrainAudioVolumeChangeEvent>)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<CheckUIInputBlockerEvent>(CheckUIInputBlockerEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<InitClientAOIEvent>(InitClientAOIEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<LocalPlayerEvent>(LocalPlayerEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<TerrainAudioVolumeChangeEvent>(TerrainAudioVolumeChangeEvent)
		// System.Void JKFrame.EventSystem.TypeEventTrigger<UpdateClientAOIEvent>(UpdateClientAOIEvent)
		// object JKFrame.PoolSystem.GetGameObject<object>(string,UnityEngine.Transform)
		// object JKFrame.ResSystem.InstantiateGameObject<object>(UnityEngine.Transform,string,bool)
		// object JKFrame.ResSystem.InstantiateGameObject<object>(string,UnityEngine.Transform,string,bool)
		// System.Void JKFrame.ResSystem.InstantiateGameObjectAsync<object>(string,System.Action<object>,UnityEngine.Transform,string,bool)
		// object JKFrame.ResSystem.LoadAsset<object>(string)
		// System.Void JKFrame.UISystem.AddUIWindowData<object>(JKFrame.UIWindowData,bool)
		// System.Void JKFrame.UISystem.Close<object>(bool)
		// object JKFrame.UISystem.GetWindow<object>()
		// object JKFrame.UISystem.Show<object>(int)
		// object LocalizationConfigBase<int>.GetContent<object>(string,int)
		// object LocalizationSystem.GetContent<object>(string,LanguageType)
		// object LocalizationSystem.GetContentByKey<object>(string,LanguageType)
		// Unity.Collections.FixedString32Bytes System.Activator.CreateInstance<Unity.Collections.FixedString32Bytes>()
		// object System.Activator.CreateInstance<object>()
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<int>(int&)
		// System.Void Unity.Netcode.BufferSerializer<object>.SerializeValue<byte>(byte&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// System.Void Unity.Netcode.BufferSerializer<object>.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.BufferSerializer<object>.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_BagExchangeItem>(C2S_BagExchangeItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_ChangeShortCutIndex>(C2S_ChangeShortCutIndex&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_Chat>(C2S_Chat&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_CraftItem>(C2S_CraftItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_Disconnect>(C2S_Disconnect&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_EnterGame>(C2S_EnterGame&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_ExchangeShortCut>(C2S_ExchangeShortCut&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_GetBagData>(C2S_GetBagData&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_Login>(C2S_Login&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_Register>(C2S_Register&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_ShopBuyItem>(C2S_ShopBuyItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_ShopSellItem>(C2S_ShopSellItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<C2S_UseItem>(C2S_UseItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_BagExchangeItem>(S2C_BagExchangeItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_BagUpdateItem>(S2C_BagUpdateItem&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_BagUpdateMoney>(S2C_BagUpdateMoney&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_ChangeShortCutIndex>(S2C_ChangeShortCutIndex&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_Chat>(S2C_Chat&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_Disconnect>(S2C_Disconnect&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_GetBagData>(S2C_GetBagData&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_Login>(S2C_Login&)
		// System.Void Unity.Netcode.FastBufferReader.ReadNetworkSerializableInPlace<S2C_Register>(S2C_Register&)
		// System.Void Unity.Netcode.FastBufferReader.ReadUnmanagedSafe<byte>(byte&)
		// System.Void Unity.Netcode.FastBufferReader.ReadUnmanagedSafe<int>(int&)
		// System.Void Unity.Netcode.FastBufferReader.ReadUnmanagedSafe<object>(object&)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<Unity.Collections.FixedString32Bytes>(Unity.Collections.FixedString32Bytes&,Unity.Netcode.FastBufferWriter.ForFixedStrings)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<byte>(byte&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferReader.ReadValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_BagExchangeItem>(C2S_BagExchangeItem&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_ChangeShortCutIndex>(C2S_ChangeShortCutIndex&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_Chat>(C2S_Chat&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_CraftItem>(C2S_CraftItem&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_Disconnect>(C2S_Disconnect&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_EnterGame>(C2S_EnterGame&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_ExchangeShortCut>(C2S_ExchangeShortCut&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_GetBagData>(C2S_GetBagData&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_Login>(C2S_Login&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_Register>(C2S_Register&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_ShopBuyItem>(C2S_ShopBuyItem&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_ShopSellItem>(C2S_ShopSellItem&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<C2S_UseItem>(C2S_UseItem&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteNetworkSerializable<object>(object&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanaged<int>(int&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanagedSafe<byte>(byte&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanagedSafe<int>(int&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteUnmanagedSafe<object>(object&)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValue<Unity.Collections.FixedString32Bytes>(Unity.Collections.FixedString32Bytes&,Unity.Netcode.FastBufferWriter.ForFixedStrings)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_BagExchangeItem>(C2S_BagExchangeItem&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_ChangeShortCutIndex>(C2S_ChangeShortCutIndex&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_Chat>(C2S_Chat&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_CraftItem>(C2S_CraftItem&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_Disconnect>(C2S_Disconnect&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_EnterGame>(C2S_EnterGame&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_ExchangeShortCut>(C2S_ExchangeShortCut&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_GetBagData>(C2S_GetBagData&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_Login>(C2S_Login&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_Register>(C2S_Register&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_ShopBuyItem>(C2S_ShopBuyItem&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_ShopSellItem>(C2S_ShopSellItem&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<C2S_UseItem>(C2S_UseItem&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<Unity.Collections.FixedString32Bytes>(Unity.Collections.FixedString32Bytes&,Unity.Netcode.FastBufferWriter.ForFixedStrings)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<byte>(byte&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.FastBufferWriter.WriteValueSafe<object>(object&,Unity.Netcode.FastBufferWriter.ForNetworkSerializable)
		// System.Void Unity.Netcode.INetworkSerializable.NetworkSerialize<Unity.Netcode.BufferSerializerReader>(Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerReader>)
		// System.Void Unity.Netcode.INetworkSerializable.NetworkSerialize<Unity.Netcode.BufferSerializerWriter>(Unity.Netcode.BufferSerializer<Unity.Netcode.BufferSerializerWriter>)
		// System.Void Unity.Netcode.IReaderWriter.SerializeValue<byte>(byte&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// System.Void Unity.Netcode.IReaderWriter.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForEnums)
		// System.Void Unity.Netcode.IReaderWriter.SerializeValue<int>(int&,Unity.Netcode.FastBufferWriter.ForPrimitives)
		// bool Unity.Netcode.NetworkVariableSerialization<Unity.Collections.FixedString32Bytes>.EqualityEquals<Unity.Collections.FixedString32Bytes>(Unity.Collections.FixedString32Bytes&,Unity.Collections.FixedString32Bytes&)
		// bool Unity.Netcode.NetworkVariableSerialization<int>.ValueEquals<int>(int&,int&)
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeEqualityChecker_UnmanagedIEquatable<Unity.Collections.FixedString32Bytes>()
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeEqualityChecker_UnmanagedValueEquals<int>()
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeSerializer_FixedString<Unity.Collections.FixedString32Bytes>()
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeSerializer_UnmanagedByMemcpy<int>()
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<object>(object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.LoadAssetAsync<object>(object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.LoadAssetWithChain<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.TrackHandle<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>)
		// object UnityEngine.Component.GetComponent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>(bool)
		// object UnityEngine.GameObject.GetComponent<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.Instantiate<object>(object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.Convert<object>()
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateChainOperation<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,System.Func<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateCompletedOperationInternal<object>(object,bool,System.Exception,bool)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateCompletedOperationWithException<object>(object,System.Exception)
		// object UnityEngine.ResourceManagement.ResourceManager.CreateOperation<object>(System.Type,int,UnityEngine.ResourceManagement.Util.IOperationCacheKey,System.Action<UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation>)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.ProvideResource<object>(UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.StartOperation<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle)
	}
}