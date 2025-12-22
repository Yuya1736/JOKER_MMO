#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StackFrameU5BU5D_tF4310E8FAB8C2853A3F0843921BF9FBEC18ABBF7;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0;
struct DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2;
struct Encoder_tAF9067231A76315584BDF4CD27990E2F485A78FA;
struct Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095;
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2;
struct ExecutionContext_t9D6EDFD92F0B2D391751963E2D77A8B03CB81710;
struct IFormatProvider_tC202922D43BFF3525109ABF3FB79625F5646AB52;
struct ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59;
struct IPrincipal_tE7AF5096287F6C3472585E124CB38FF2A51EAB5F;
struct InternalThread_tF40B7BFCBD60C82BD8475A22FF5186CA10293687;
struct LocalDataStoreHolder_t789DD474AE5141213C2105CE57830ECFC2D3C03F;
struct LocalDataStoreMgr_t205F1783D5CC2B148E829B5882E5406FF9A3AC1E;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodBase_t;
struct MethodInfo_t;
struct MulticastDelegate_t;
struct StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443;
struct StackTrace_t7C150C7C14136F985311A83A93524B1019F70853;
struct Stream_tF844051B786E8F7F4244DBD218D74E8617B9A2DE;
struct StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4;
struct String_t;
struct StringBuilder_t;
struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572;
struct TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3;
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F;
struct Type_t;
struct UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LogType_t75E8B2E00BA95C0147BB7685DB82B45D18DE6D03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StackTrace_t7C150C7C14136F985311A83A93524B1019F70853_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringBuilder_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral00B28FF06B788B9B67C6B259800F404F9F3761FD;
IL2CPP_EXTERN_C String_t* _stringLiteral0400D8B9C396E020DD13A21F3C0904904C8B65FB;
IL2CPP_EXTERN_C String_t* _stringLiteral05BE11CA8745D9E40F7E780C852B45025FB15804;
IL2CPP_EXTERN_C String_t* _stringLiteral1D0C41FB6C602D437386B6456E644B635D84A6D3;
IL2CPP_EXTERN_C String_t* _stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94;
IL2CPP_EXTERN_C String_t* _stringLiteral3A7510618F1AE82AB3D136A541FD06575B7C440C;
IL2CPP_EXTERN_C String_t* _stringLiteral5F6D306B472BA043198C8702807F50258EDF6AC6;
IL2CPP_EXTERN_C String_t* _stringLiteral69CD20F6B81B7186F21DADE7B22D63C6616C2FFA;
IL2CPP_EXTERN_C String_t* _stringLiteral840636931AAC446C107047AD1C2240E9D4CD4504;
IL2CPP_EXTERN_C String_t* _stringLiteral950957F1F27C3060FE67E403AD610B4C3628FC8D;
IL2CPP_EXTERN_C String_t* _stringLiteralA3B4526232D4520824F6B8BBC9F0E868057DCC5D;
IL2CPP_EXTERN_C String_t* _stringLiteralB2A649209AE1998E0B4A2FCC12F79B9D1FB88DC1;
IL2CPP_EXTERN_C String_t* _stringLiteralB37194749AB0C6A6CA879037B7990429FD72AAFB;
IL2CPP_EXTERN_C String_t* _stringLiteralD2185E2B320102DBAC16E15976BE9935D7508BC0;
IL2CPP_EXTERN_C String_t* _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
IL2CPP_EXTERN_C String_t* _stringLiteralEE382DA93CD20989C0D6C5E2E89DE480F4E4350A;
IL2CPP_EXTERN_C String_t* _stringLiteralEFBC31607D26CA3742A0578CFF02836BF53950D4;
IL2CPP_EXTERN_C const RuntimeMethod* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* RuntimeObject_0_0_0_var;

struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t2E6E0872D040B3E43F24C89B1ED2C25791D00F00 
{
};
struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE  : public RuntimeObject
{
};
struct ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014  : public RuntimeObject
{
};
struct CriticalFinalizerObject_t1DCAB623CAEA6529A96F5F3EDE3C7048A6E313C9  : public RuntimeObject
{
};
struct JKLog_t68B24504555304363AF6B7BF0F20F4979B002663  : public RuntimeObject
{
};
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE  : public RuntimeObject
{
	RuntimeObject* ____identity;
};
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_pinvoke
{
	Il2CppIUnknown* ____identity;
};
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_com
{
	Il2CppIUnknown* ____identity;
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443  : public RuntimeObject
{
	int32_t ___ilOffset;
	int32_t ___nativeOffset;
	int64_t ___methodAddress;
	uint32_t ___methodIndex;
	MethodBase_t* ___methodBase;
	String_t* ___fileName;
	int32_t ___lineNumber;
	int32_t ___columnNumber;
	String_t* ___internalMethodName;
};
struct StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443_marshaled_pinvoke
{
	int32_t ___ilOffset;
	int32_t ___nativeOffset;
	int64_t ___methodAddress;
	uint32_t ___methodIndex;
	MethodBase_t* ___methodBase;
	char* ___fileName;
	int32_t ___lineNumber;
	int32_t ___columnNumber;
	char* ___internalMethodName;
};
struct StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443_marshaled_com
{
	int32_t ___ilOffset;
	int32_t ___nativeOffset;
	int64_t ___methodAddress;
	uint32_t ___methodIndex;
	MethodBase_t* ___methodBase;
	Il2CppChar* ___fileName;
	int32_t ___lineNumber;
	int32_t ___columnNumber;
	Il2CppChar* ___internalMethodName;
};
struct StackTrace_t7C150C7C14136F985311A83A93524B1019F70853  : public RuntimeObject
{
	StackFrameU5BU5D_tF4310E8FAB8C2853A3F0843921BF9FBEC18ABBF7* ___frames;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	bool ___debug_info;
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct StringBuilder_t  : public RuntimeObject
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___m_ChunkChars;
	StringBuilder_t* ___m_ChunkPrevious;
	int32_t ___m_ChunkLength;
	int32_t ___m_ChunkOffset;
	int32_t ___m_MaxCapacity;
};
struct UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C  : public RuntimeObject
{
	MethodInfo_t* ___logFunction;
	MethodInfo_t* ___errorFunction;
	MethodInfo_t* ___warningFunction;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D 
{
	uint64_t ____dateData;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct FileSystemInfo_tE3063B9229F46B05A5F6D018C8C4CA510104E8E9  : public MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE
{
	int32_t ____dataInitialized;
	String_t* ___FullPath;
	String_t* ___OriginalPath;
	String_t* ____name;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct MethodBase_t  : public MemberInfo_t
{
};
struct TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3  : public MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___CoreNewLine;
	String_t* ___CoreNewLineStr;
	RuntimeObject* ____internalFormatProvider;
};
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F  : public CriticalFinalizerObject_t1DCAB623CAEA6529A96F5F3EDE3C7048A6E313C9
{
	InternalThread_tF40B7BFCBD60C82BD8475A22FF5186CA10293687* ___internal_thread;
	RuntimeObject* ___m_ThreadStartArg;
	RuntimeObject* ___pending_exception;
	MulticastDelegate_t* ___m_Delegate;
	ExecutionContext_t9D6EDFD92F0B2D391751963E2D77A8B03CB81710* ___m_ExecutionContext;
	bool ___m_ExecutionContextBelongsToOuterScope;
	RuntimeObject* ___principal;
	int32_t ___principal_version;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct ConsoleColor_tF1C4F0AD2193FB7B4B0903ACA588E0EA0DBABD2F 
{
	int32_t ___value__;
};
struct DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2  : public FileSystemInfo_tE3063B9229F46B05A5F6D018C8C4CA510104E8E9
{
};
struct LogType_t75E8B2E00BA95C0147BB7685DB82B45D18DE6D03 
{
	int32_t ___value__;
};
struct LoggerType_t5D97307A349AEE2C1E6F59C26F61F9755E09E0AF 
{
	int32_t ___value__;
};
struct MethodInfo_t  : public MethodBase_t
{
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4  : public TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3
{
	Stream_tF844051B786E8F7F4244DBD218D74E8617B9A2DE* ____stream;
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ____encoding;
	Encoder_tAF9067231A76315584BDF4CD27990E2F485A78FA* ____encoder;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ____byteBuffer;
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ____charBuffer;
	int32_t ____charPos;
	int32_t ____charLen;
	bool ____autoFlush;
	bool ____haveWrittenPreamble;
	bool ____closable;
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ____asyncWriteTask;
};
struct StringSplitOptions_t4DD892C76C70DD4800FC1B76054D69826F770062 
{
	int32_t ___value__;
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___Value;
};
struct JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields
{
	RuntimeObject* ___m_Logger;
	bool ___m_WriteTime;
	bool ___m_WriteThreadID;
	bool ___m_WriteTrace;
	bool ___m_EnableSave;
	String_t* ___m_SavePath;
	bool ___m_UseCustomSaveFileName;
	String_t* ___m_CustomSaveFileName;
	int32_t ___m_SkipTraceFrameCount;
	int32_t ___m_WantSaveLogTypes;
	String_t* ___fileSavePath;
	StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* ___logStreamWriter;
};
struct StackTrace_t7C150C7C14136F985311A83A93524B1019F70853_StaticFields
{
	bool ___isAotidSet;
	String_t* ___aotid;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_StaticFields
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth365;
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth366;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MinValue;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MaxValue;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___UnixEpoch;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticFields
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___enumSeperatorCharArray;
};
struct TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3_StaticFields
{
	TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3* ___Null;
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___s_coreNewLine;
};
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F_StaticFields
{
	LocalDataStoreMgr_t205F1783D5CC2B148E829B5882E5406FF9A3AC1E* ___s_LocalDataStoreMgr;
	AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD* ___s_asyncLocalCurrentCulture;
	AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD* ___s_asyncLocalCurrentUICulture;
};
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F_ThreadStaticFields
{
	LocalDataStoreHolder_t789DD474AE5141213C2105CE57830ECFC2D3C03F* ___s_LocalDataStore;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___m_CurrentCulture;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___m_CurrentUICulture;
	Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* ___current_thread;
};
struct StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4_StaticFields
{
	StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* ___Null;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB  : public RuntimeArray
{
	ALIGN_FIELD (8) Type_t* m_Items[1];

	inline Type_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Type_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Type_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Type_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Type_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Type_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F (int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Console_WriteLine_m77CEDA0C084428F0D6220988DA66992EC1925AEA (String_t* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger__ctor_m21A32F001A056D6315D385ADBEEBD2BCC46F99FE (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrWhiteSpace_m42E1F3B2C358068D645E46F01CF1834DC77A5A10 (String_t* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Close_m3375E4C056736E1B427EB317C8537F65A52741F7 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DateTime_ToString_m6963A84785C320DA776C9FCFFEDAF26C8F1A8D78 (DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D* __this, String_t* ___0_format, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Directory_Exists_m3D125E9E88C291CF11113444F961A64DD83AE1C7 (String_t* ___0_path, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A (String_t* ___0_path, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void File_Delete_mE29829DA504F3E1B8BCB78F21E2862C9ED7EC386 (String_t* ___0_path, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2* Directory_CreateDirectory_m16EC5CE8561A997C6635E06DC24C77590F29D94F (String_t* ___0_path, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* File_AppendText_m60AE4CD09FE3C6B1FEC2A5E9291A3B12C5385339 (String_t* ___0_path, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m2619CA8D2C3476DF1A302D9D941498BB1C6164C5 (StringBuilder_t* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F (StringBuilder_t* __this, String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_GetThreadID_mA28754CE532B4CD5017DCD77CB6B412C500D2B22 (const RuntimeMethod* method) ;
inline ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline (const RuntimeMethod* method)
{
	return ((  ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* (*) (const RuntimeMethod*))Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_AppendFormat_m14CB447291E6149BCF32E5E37DA21514BAD9C151 (StringBuilder_t* __this, String_t* ___0_format, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_GetTrace_m3DA557D3E59B61DE3D2ADF894BDAEC3029B4605F (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* Thread_get_CurrentThread_m6D4719F4993DB9200490531FF02D4076FF9CA9BD (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Thread_get_ManagedThreadId_m74ACB74A574EE535C2B00B7D64F203A62E796B05 (Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8 (String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void StackTrace__ctor_m7C03595A7D2ADA64E7CB5311C9563AF588DC8480 (StackTrace_t7C150C7C14136F985311A83A93524B1019F70853* __this, int32_t ___0_skipFrames, bool ___1_fNeedFileInfo, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, RuntimeObject* ___3_arg2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937 (int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_DecorateLog_mA157793195C1E27CC38793DC6C2D93E90F39A3DA (int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_WriteToFile_mB18F238D16F79B9B6D8BC24DC90404D00E5A466C (String_t* ___0_text, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextWriter_Dispose_m5B2CA4D250335AB11031AFC7F202AA5B7A70C4D7 (TextWriter_tA9E5461506CF806E17B6BBBF2119359DEDA3F0F3* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR MethodInfo_t* Type_GetMethod_m9E8E55EC8316CE8A2851B62AD4C73E841FEAC2EA (Type_t* __this, String_t* ___0_name, TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___1_types, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MethodBase_Invoke_mEEF3218648F111A8C338001A7804091A0747C826 (MethodBase_t* __this, RuntimeObject* ___0_obj, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_parameters, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UnityLogger_Decorate_m6C8219FED6ED8106C0DF18F3BA1DFA4C02D92E47 (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* String_Split_m03F46561E2DF46D1E3AE749A77706EFC2F6488F4 (String_t* __this, StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___0_separator, int32_t ___1_options, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D (StringBuilder_t* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger_Error_mA0F14A0316658A4E780DC6EF6D1D125792691EAC (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)12), NULL);
		String_t* L_0 = ___0_msg;
		Console_WriteLine_m77CEDA0C084428F0D6220988DA66992EC1925AEA(L_0, NULL);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)15), NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger_Log_m031B82085CC546D0E2B848E0786790A3BC18620F (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___0_msg;
		il2cpp_codegen_runtime_class_init_inline(Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		Console_WriteLine_m77CEDA0C084428F0D6220988DA66992EC1925AEA(L_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger_Succeed_m0367BE0DF0EF420187E57530678E9F42DAA74908 (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)10), NULL);
		String_t* L_0 = ___0_msg;
		Console_WriteLine_m77CEDA0C084428F0D6220988DA66992EC1925AEA(L_0, NULL);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)15), NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger_Warning_m383DB88BE4ED1721FA38AE02A694AFE0D4CF4811 (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)14), NULL);
		String_t* L_0 = ___0_msg;
		Console_WriteLine_m77CEDA0C084428F0D6220988DA66992EC1925AEA(L_0, NULL);
		Console_set_ForegroundColor_m4332BA76D4ECCFF08C9C71F05C4D47EA83EC176F(((int32_t)15), NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsoleLogger__ctor_m21A32F001A056D6315D385ADBEEBD2BCC46F99FE (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Init_m24510C9977F2475E09AE19A7C65984CDB84A7CA2 (int32_t ___0_loggerType, bool ___1_writeTime, bool ___2_writeThreadID, bool ___3_writeTrace, bool ___4_enableSave, String_t* ___5_savePath, String_t* ___6_saveFileName, int32_t ___7_wantSaveLogTypes, int32_t ___8_skipTraceFrameCount, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1D0C41FB6C602D437386B6456E644B635D84A6D3);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEFBC31607D26CA3742A0578CFF02836BF53950D4);
		s_Il2CppMethodInitialized = true;
	}
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_loggerType;
		if (!L_0)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_1 = ___0_loggerType;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0015;
		}
	}
	{
		goto IL_001f;
	}

IL_0009:
	{
		UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* L_2 = (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C*)il2cpp_codegen_object_new(UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C_il2cpp_TypeInfo_var);
		UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E(L_2, NULL);
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger), (void*)L_2);
		goto IL_001f;
	}

IL_0015:
	{
		ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014* L_3 = (ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014*)il2cpp_codegen_object_new(ConsoleLogger_tCBC271CB56664D38005B03A72FF12BFC06BF7014_il2cpp_TypeInfo_var);
		ConsoleLogger__ctor_m21A32F001A056D6315D385ADBEEBD2BCC46F99FE(L_3, NULL);
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger), (void*)L_3);
	}

IL_001f:
	{
		bool L_4 = ___1_writeTime;
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTime = L_4;
		bool L_5 = ___2_writeThreadID;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteThreadID = L_5;
		bool L_6 = ___3_writeTrace;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTrace = L_6;
		bool L_7 = ___4_enableSave;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_EnableSave = L_7;
		String_t* L_8 = ___5_savePath;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath), (void*)L_8);
		String_t* L_9 = ___6_saveFileName;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName), (void*)L_9);
		String_t* L_10 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName;
		bool L_11;
		L_11 = String_IsNullOrWhiteSpace_m42E1F3B2C358068D645E46F01CF1834DC77A5A10(L_10, NULL);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_UseCustomSaveFileName = (bool)((((int32_t)L_11) == ((int32_t)0))? 1 : 0);
		int32_t L_12 = ___8_skipTraceFrameCount;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SkipTraceFrameCount = L_12;
		int32_t L_13 = ___7_wantSaveLogTypes;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WantSaveLogTypes = L_13;
		JKLog_Close_m3375E4C056736E1B427EB317C8537F65A52741F7(NULL);
		bool L_14 = ___4_enableSave;
		if (!L_14)
		{
			goto IL_0103;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		bool L_15 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_UseCustomSaveFileName;
		if (!L_15)
		{
			goto IL_0094;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_16 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath;
		String_t* L_17 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName;
		String_t* L_18;
		L_18 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(L_16, L_17, _stringLiteral1D0C41FB6C602D437386B6456E644B635D84A6D3, NULL);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath = L_18;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath), (void*)L_18);
		goto IL_00ba;
	}

IL_0094:
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_19 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath;
		il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_20;
		L_20 = DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C(NULL);
		V_0 = L_20;
		String_t* L_21;
		L_21 = DateTime_ToString_m6963A84785C320DA776C9FCFFEDAF26C8F1A8D78((&V_0), _stringLiteralEFBC31607D26CA3742A0578CFF02836BF53950D4, NULL);
		String_t* L_22;
		L_22 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(L_19, L_21, _stringLiteral1D0C41FB6C602D437386B6456E644B635D84A6D3, NULL);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath = L_22;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath), (void*)L_22);
	}

IL_00ba:
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_23 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath;
		bool L_24;
		L_24 = Directory_Exists_m3D125E9E88C291CF11113444F961A64DD83AE1C7(L_23, NULL);
		if (!L_24)
		{
			goto IL_00de;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_25 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath;
		bool L_26;
		L_26 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_25, NULL);
		if (!L_26)
		{
			goto IL_00e9;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_27 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath;
		File_Delete_mE29829DA504F3E1B8BCB78F21E2862C9ED7EC386(L_27, NULL);
		goto IL_00e9;
	}

IL_00de:
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_28 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath;
		DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2* L_29;
		L_29 = Directory_CreateDirectory_m16EC5CE8561A997C6635E06DC24C77590F29D94F(L_28, NULL);
	}

IL_00e9:
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_30 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___fileSavePath;
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_31;
		L_31 = File_AppendText_m60AE4CD09FE3C6B1FEC2A5E9291A3B12C5385339(L_30, NULL);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter = L_31;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter), (void*)L_31);
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_32 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter;
		NullCheck(L_32);
		VirtualActionInvoker1< bool >::Invoke(17, L_32, (bool)1);
	}

IL_0103:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_DecorateLog_mA157793195C1E27CC38793DC6C2D93E90F39A3DA (int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LogType_t75E8B2E00BA95C0147BB7685DB82B45D18DE6D03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral05BE11CA8745D9E40F7E780C852B45025FB15804);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5F6D306B472BA043198C8702807F50258EDF6AC6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral950957F1F27C3060FE67E403AD610B4C3628FC8D);
		s_Il2CppMethodInitialized = true;
	}
	StringBuilder_t* V_0 = NULL;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		StringBuilder_t* L_0 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		StringBuilder__ctor_m2619CA8D2C3476DF1A302D9D941498BB1C6164C5(L_0, ((int32_t)100), NULL);
		V_0 = L_0;
		StringBuilder_t* L_1 = V_0;
		Il2CppFakeBox<int32_t> L_2(LogType_t75E8B2E00BA95C0147BB7685DB82B45D18DE6D03_il2cpp_TypeInfo_var, (&___0_logType));
		String_t* L_3;
		L_3 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_2), NULL);
		NullCheck(L_1);
		StringBuilder_t* L_4;
		L_4 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_1, _stringLiteral950957F1F27C3060FE67E403AD610B4C3628FC8D, L_3, NULL);
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		bool L_5 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTime;
		if (!L_5)
		{
			goto IL_0046;
		}
	}
	{
		StringBuilder_t* L_6 = V_0;
		il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_7;
		L_7 = DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C(NULL);
		V_1 = L_7;
		String_t* L_8;
		L_8 = DateTime_ToString_m6963A84785C320DA776C9FCFFEDAF26C8F1A8D78((&V_1), _stringLiteral5F6D306B472BA043198C8702807F50258EDF6AC6, NULL);
		NullCheck(L_6);
		StringBuilder_t* L_9;
		L_9 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_6, _stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94, L_8, NULL);
	}

IL_0046:
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		bool L_10 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteThreadID;
		if (!L_10)
		{
			goto IL_005e;
		}
	}
	{
		StringBuilder_t* L_11 = V_0;
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_12;
		L_12 = JKLog_GetThreadID_mA28754CE532B4CD5017DCD77CB6B412C500D2B22(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13;
		L_13 = Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline(Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		NullCheck(L_11);
		StringBuilder_t* L_14;
		L_14 = StringBuilder_AppendFormat_m14CB447291E6149BCF32E5E37DA21514BAD9C151(L_11, L_12, L_13, NULL);
	}

IL_005e:
	{
		StringBuilder_t* L_15 = V_0;
		String_t* L_16 = ___1_msg;
		NullCheck(L_15);
		StringBuilder_t* L_17;
		L_17 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_15, _stringLiteral05BE11CA8745D9E40F7E780C852B45025FB15804, L_16, NULL);
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		bool L_18 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTrace;
		if (!L_18)
		{
			goto IL_0083;
		}
	}
	{
		StringBuilder_t* L_19 = V_0;
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_20;
		L_20 = JKLog_GetTrace_m3DA557D3E59B61DE3D2ADF894BDAEC3029B4605F(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_21;
		L_21 = Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline(Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		NullCheck(L_19);
		StringBuilder_t* L_22;
		L_22 = StringBuilder_AppendFormat_m14CB447291E6149BCF32E5E37DA21514BAD9C151(L_19, L_20, L_21, NULL);
	}

IL_0083:
	{
		StringBuilder_t* L_23 = V_0;
		NullCheck(L_23);
		String_t* L_24;
		L_24 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_23);
		return L_24;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_GetThreadID_mA28754CE532B4CD5017DCD77CB6B412C500D2B22 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral69CD20F6B81B7186F21DADE7B22D63C6616C2FFA);
		s_Il2CppMethodInitialized = true;
	}
	{
		Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* L_0;
		L_0 = Thread_get_CurrentThread_m6D4719F4993DB9200490531FF02D4076FF9CA9BD(NULL);
		NullCheck(L_0);
		int32_t L_1;
		L_1 = Thread_get_ManagedThreadId_m74ACB74A574EE535C2B00B7D64F203A62E796B05(L_0, NULL);
		int32_t L_2 = L_1;
		RuntimeObject* L_3 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_2);
		String_t* L_4;
		L_4 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(_stringLiteral69CD20F6B81B7186F21DADE7B22D63C6616C2FFA, L_3, NULL);
		return L_4;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_GetTrace_m3DA557D3E59B61DE3D2ADF894BDAEC3029B4605F (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StackTrace_t7C150C7C14136F985311A83A93524B1019F70853_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB37194749AB0C6A6CA879037B7990429FD72AAFB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		s_Il2CppMethodInitialized = true;
	}
	StackTrace_t7C150C7C14136F985311A83A93524B1019F70853* V_0 = NULL;
	String_t* V_1 = NULL;
	int32_t V_2 = 0;
	StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443* V_3 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		int32_t L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SkipTraceFrameCount;
		StackTrace_t7C150C7C14136F985311A83A93524B1019F70853* L_1 = (StackTrace_t7C150C7C14136F985311A83A93524B1019F70853*)il2cpp_codegen_object_new(StackTrace_t7C150C7C14136F985311A83A93524B1019F70853_il2cpp_TypeInfo_var);
		StackTrace__ctor_m7C03595A7D2ADA64E7CB5311C9563AF588DC8480(L_1, L_0, (bool)1, NULL);
		V_0 = L_1;
		V_1 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		V_2 = 0;
		goto IL_004a;
	}

IL_0016:
	{
		StackTrace_t7C150C7C14136F985311A83A93524B1019F70853* L_2 = V_0;
		int32_t L_3 = V_2;
		NullCheck(L_2);
		StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443* L_4;
		L_4 = VirtualFuncInvoker1< StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443*, int32_t >::Invoke(5, L_2, L_3);
		V_3 = L_4;
		String_t* L_5 = V_1;
		StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443* L_6 = V_3;
		NullCheck(L_6);
		String_t* L_7;
		L_7 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_6);
		StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443* L_8 = V_3;
		NullCheck(L_8);
		MethodBase_t* L_9;
		L_9 = VirtualFuncInvoker0< MethodBase_t* >::Invoke(7, L_8);
		StackFrame_tB901270D6679ED5D24D872C25D4AD053F22F3443* L_10 = V_3;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_10);
		int32_t L_12 = L_11;
		RuntimeObject* L_13 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_12);
		String_t* L_14;
		L_14 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteralB37194749AB0C6A6CA879037B7990429FD72AAFB, L_7, L_9, L_13, NULL);
		String_t* L_15;
		L_15 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(L_5, L_14, NULL);
		V_1 = L_15;
		int32_t L_16 = V_2;
		V_2 = ((int32_t)il2cpp_codegen_add(L_16, 1));
	}

IL_004a:
	{
		int32_t L_17 = V_2;
		StackTrace_t7C150C7C14136F985311A83A93524B1019F70853* L_18 = V_0;
		NullCheck(L_18);
		int32_t L_19;
		L_19 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_18);
		if ((((int32_t)L_17) < ((int32_t)L_19)))
		{
			goto IL_0016;
		}
	}
	{
		String_t* L_20 = V_1;
		return L_20;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Log_m5DD0AA7426A06D69CF783B640F8FF527F3986B1F (String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		RuntimeObject* L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger;
		String_t* L_1 = ___0_msg;
		String_t* L_2;
		L_2 = JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937(1, L_1, NULL);
		NullCheck(L_0);
		InterfaceActionInvoker1< String_t* >::Invoke(0, ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var, L_0, L_2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Warning_m66F6212C95FF8D92FEEDCA48EA68E3123E80225C (String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		RuntimeObject* L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger;
		String_t* L_1 = ___0_msg;
		String_t* L_2;
		L_2 = JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937(2, L_1, NULL);
		NullCheck(L_0);
		InterfaceActionInvoker1< String_t* >::Invoke(1, ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var, L_0, L_2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Error_m5F2C97346552329D1FF9B5566FC9DCE68A4AD240 (String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		RuntimeObject* L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger;
		String_t* L_1 = ___0_msg;
		String_t* L_2;
		L_2 = JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937(4, L_1, NULL);
		NullCheck(L_0);
		InterfaceActionInvoker1< String_t* >::Invoke(2, ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var, L_0, L_2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Succeed_m58F98F2D8BAC7482554B9AE6B92932461F44AA39 (String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		RuntimeObject* L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_Logger;
		String_t* L_1 = ___0_msg;
		String_t* L_2;
		L_2 = JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937(8, L_1, NULL);
		NullCheck(L_0);
		InterfaceActionInvoker1< String_t* >::Invoke(3, ILogger_t690D0ACCA95A549161556B45207D9C52CF477C59_il2cpp_TypeInfo_var, L_0, L_2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* JKLog_DecorateAndWriteLog_m41EDF92B85CC129B9E44DF4F5E2DA644F523A937 (int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	{
		int32_t L_0 = ___0_logType;
		String_t* L_1 = ___1_msg;
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		String_t* L_2;
		L_2 = JKLog_DecorateLog_mA157793195C1E27CC38793DC6C2D93E90F39A3DA(L_0, L_1, NULL);
		V_0 = L_2;
		int32_t L_3 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WantSaveLogTypes;
		int32_t L_4 = ___0_logType;
		if (!((int32_t)((int32_t)L_3&(int32_t)L_4)))
		{
			goto IL_0017;
		}
	}
	{
		String_t* L_5 = V_0;
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		JKLog_WriteToFile_mB18F238D16F79B9B6D8BC24DC90404D00E5A466C(L_5, NULL);
	}

IL_0017:
	{
		String_t* L_6 = V_0;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_WriteToFile_mB18F238D16F79B9B6D8BC24DC90404D00E5A466C (String_t* ___0_text, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		bool L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_EnableSave;
		if (!L_0)
		{
			goto IL_0019;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_1 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_2 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter;
		String_t* L_3 = ___0_text;
		NullCheck(L_2);
		VirtualActionInvoker1< String_t* >::Invoke(14, L_2, L_3);
	}

IL_0019:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog_Close_m3375E4C056736E1B427EB317C8537F65A52741F7 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* G_B2_0 = NULL;
	StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* G_B1_0 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_0 = ((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___logStreamWriter;
		StreamWriter_t6E7DF7D524AA3C018A65F62EE80779873ED4D1E4* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000a;
		}
		G_B1_0 = L_1;
	}
	{
		return;
	}

IL_000a:
	{
		NullCheck(G_B2_0);
		TextWriter_Dispose_m5B2CA4D250335AB11031AFC7F202AA5B7A70C4D7(G_B2_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void JKLog__cctor_m09E5D8C964C7551A3D03F2AEE3E0BE621E9014F8 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTime = (bool)1;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteThreadID = (bool)1;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_WriteTrace = (bool)1;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_EnableSave = (bool)0;
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_SavePath), (void*)L_0);
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_UseCustomSaveFileName = (bool)0;
		String_t* L_1 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty;
		((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_StaticFields*)il2cpp_codegen_static_fields_for(JKLog_t68B24504555304363AF6B7BF0F20F4979B002663_il2cpp_TypeInfo_var))->___m_CustomSaveFileName), (void*)L_1);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeObject_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3A7510618F1AE82AB3D136A541FD06575B7C440C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral840636931AAC446C107047AD1C2240E9D4CD4504);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD2185E2B320102DBAC16E15976BE9935D7508BC0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEE382DA93CD20989C0D6C5E2E89DE480F4E4350A);
		s_Il2CppMethodInitialized = true;
	}
	Type_t* V_0 = NULL;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_0;
		L_0 = il2cpp_codegen_get_type(_stringLiteral840636931AAC446C107047AD1C2240E9D4CD4504, Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var, UnityLogger__ctor_m6BDDB3006D5092B005A55D6D12C12B0783D7488E_RuntimeMethod_var);
		V_0 = L_0;
		Type_t* L_1 = V_0;
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_2 = (TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)SZArrayNew(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB_il2cpp_TypeInfo_var, (uint32_t)1);
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_3 = L_2;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (RuntimeObject_0_0_0_var) };
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		NullCheck(L_3);
		ArrayElementTypeCheck (L_3, L_5);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(0), (Type_t*)L_5);
		NullCheck(L_1);
		MethodInfo_t* L_6;
		L_6 = Type_GetMethod_m9E8E55EC8316CE8A2851B62AD4C73E841FEAC2EA(L_1, _stringLiteralD2185E2B320102DBAC16E15976BE9935D7508BC0, L_3, NULL);
		__this->___logFunction = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___logFunction), (void*)L_6);
		Type_t* L_7 = V_0;
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_8 = (TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)SZArrayNew(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB_il2cpp_TypeInfo_var, (uint32_t)1);
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_9 = L_8;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_10 = { reinterpret_cast<intptr_t> (RuntimeObject_0_0_0_var) };
		Type_t* L_11;
		L_11 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_10, NULL);
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_11);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(0), (Type_t*)L_11);
		NullCheck(L_7);
		MethodInfo_t* L_12;
		L_12 = Type_GetMethod_m9E8E55EC8316CE8A2851B62AD4C73E841FEAC2EA(L_7, _stringLiteralEE382DA93CD20989C0D6C5E2E89DE480F4E4350A, L_9, NULL);
		__this->___errorFunction = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___errorFunction), (void*)L_12);
		Type_t* L_13 = V_0;
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_14 = (TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB*)SZArrayNew(TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB_il2cpp_TypeInfo_var, (uint32_t)1);
		TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* L_15 = L_14;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_16 = { reinterpret_cast<intptr_t> (RuntimeObject_0_0_0_var) };
		Type_t* L_17;
		L_17 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_16, NULL);
		NullCheck(L_15);
		ArrayElementTypeCheck (L_15, L_17);
		(L_15)->SetAt(static_cast<il2cpp_array_size_t>(0), (Type_t*)L_17);
		NullCheck(L_13);
		MethodInfo_t* L_18;
		L_18 = Type_GetMethod_m9E8E55EC8316CE8A2851B62AD4C73E841FEAC2EA(L_13, _stringLiteral3A7510618F1AE82AB3D136A541FD06575B7C440C, L_15, NULL);
		__this->___warningFunction = L_18;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___warningFunction), (void*)L_18);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger_Log_m20FFABDE80AE526377A20153717181D1796D36A0 (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		MethodInfo_t* L_0 = __this->___logFunction;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		String_t* L_3 = ___0_msg;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		NullCheck(L_0);
		RuntimeObject* L_4;
		L_4 = MethodBase_Invoke_mEEF3218648F111A8C338001A7804091A0747C826(L_0, NULL, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger_Error_m2D258A6E57A1A7A22503C391147F1DFBCE5881EA (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___0_msg;
		String_t* L_1;
		L_1 = UnityLogger_Decorate_m6C8219FED6ED8106C0DF18F3BA1DFA4C02D92E47(__this, 4, L_0, NULL);
		___0_msg = L_1;
		MethodInfo_t* L_2 = __this->___errorFunction;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_3;
		String_t* L_5 = ___0_msg;
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_5);
		NullCheck(L_2);
		RuntimeObject* L_6;
		L_6 = MethodBase_Invoke_mEEF3218648F111A8C338001A7804091A0747C826(L_2, NULL, L_4, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger_Succeed_mB48E23D6FA6FA1C8B6C05647B9EE19E41819AC06 (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___0_msg;
		String_t* L_1;
		L_1 = UnityLogger_Decorate_m6C8219FED6ED8106C0DF18F3BA1DFA4C02D92E47(__this, 8, L_0, NULL);
		___0_msg = L_1;
		MethodInfo_t* L_2 = __this->___logFunction;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_3;
		String_t* L_5 = ___0_msg;
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_5);
		NullCheck(L_2);
		RuntimeObject* L_6;
		L_6 = MethodBase_Invoke_mEEF3218648F111A8C338001A7804091A0747C826(L_2, NULL, L_4, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityLogger_Warning_m0270753EC7A595D3331739C3E771FA2CA7E6C87F (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, String_t* ___0_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___0_msg;
		String_t* L_1;
		L_1 = UnityLogger_Decorate_m6C8219FED6ED8106C0DF18F3BA1DFA4C02D92E47(__this, 2, L_0, NULL);
		___0_msg = L_1;
		MethodInfo_t* L_2 = __this->___warningFunction;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_3;
		String_t* L_5 = ___0_msg;
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_5);
		NullCheck(L_2);
		RuntimeObject* L_6;
		L_6 = MethodBase_Invoke_mEEF3218648F111A8C338001A7804091A0747C826(L_2, NULL, L_4, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UnityLogger_Decorate_m6C8219FED6ED8106C0DF18F3BA1DFA4C02D92E47 (UnityLogger_tF61641C24B086EA71D3D76D693B2761F98C55C1C* __this, int32_t ___0_logType, String_t* ___1_msg, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral00B28FF06B788B9B67C6B259800F404F9F3761FD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0400D8B9C396E020DD13A21F3C0904904C8B65FB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA3B4526232D4520824F6B8BBC9F0E868057DCC5D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB2A649209AE1998E0B4A2FCC12F79B9D1FB88DC1);
		s_Il2CppMethodInitialized = true;
	}
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* V_0 = NULL;
	StringBuilder_t* V_1 = NULL;
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* V_2 = NULL;
	int32_t V_3 = 0;
	String_t* V_4 = NULL;
	String_t* V_5 = NULL;
	String_t* V_6 = NULL;
	{
		String_t* L_0 = ___1_msg;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_1 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)1);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_2 = L_1;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)_stringLiteral00B28FF06B788B9B67C6B259800F404F9F3761FD);
		NullCheck(L_0);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_3;
		L_3 = String_Split_m03F46561E2DF46D1E3AE749A77706EFC2F6488F4(L_0, L_2, 0, NULL);
		V_0 = L_3;
		StringBuilder_t* L_4 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_4, NULL);
		V_1 = L_4;
		int32_t L_5 = ___0_logType;
		if ((((int32_t)L_5) == ((int32_t)2)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = ___0_logType;
		if ((((int32_t)L_6) == ((int32_t)4)))
		{
			goto IL_004f;
		}
	}
	{
		int32_t L_7 = ___0_logType;
		if ((((int32_t)L_7) == ((int32_t)8)))
		{
			goto IL_0074;
		}
	}
	{
		goto IL_0097;
	}

IL_002a:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_8 = V_0;
		V_2 = L_8;
		V_3 = 0;
		goto IL_0047;
	}

IL_0030:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = V_2;
		int32_t L_10 = V_3;
		NullCheck(L_9);
		int32_t L_11 = L_10;
		String_t* L_12 = (L_9)->GetAt(static_cast<il2cpp_array_size_t>(L_11));
		V_4 = L_12;
		StringBuilder_t* L_13 = V_1;
		String_t* L_14 = V_4;
		NullCheck(L_13);
		StringBuilder_t* L_15;
		L_15 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_13, _stringLiteral0400D8B9C396E020DD13A21F3C0904904C8B65FB, L_14, NULL);
		int32_t L_16 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_16, 1));
	}

IL_0047:
	{
		int32_t L_17 = V_3;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_18 = V_2;
		NullCheck(L_18);
		if ((((int32_t)L_17) < ((int32_t)((int32_t)(((RuntimeArray*)L_18)->max_length)))))
		{
			goto IL_0030;
		}
	}
	{
		goto IL_0097;
	}

IL_004f:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_19 = V_0;
		V_2 = L_19;
		V_3 = 0;
		goto IL_006c;
	}

IL_0055:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_20 = V_2;
		int32_t L_21 = V_3;
		NullCheck(L_20);
		int32_t L_22 = L_21;
		String_t* L_23 = (L_20)->GetAt(static_cast<il2cpp_array_size_t>(L_22));
		V_5 = L_23;
		StringBuilder_t* L_24 = V_1;
		String_t* L_25 = V_5;
		NullCheck(L_24);
		StringBuilder_t* L_26;
		L_26 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_24, _stringLiteralA3B4526232D4520824F6B8BBC9F0E868057DCC5D, L_25, NULL);
		int32_t L_27 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_27, 1));
	}

IL_006c:
	{
		int32_t L_28 = V_3;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_29 = V_2;
		NullCheck(L_29);
		if ((((int32_t)L_28) < ((int32_t)((int32_t)(((RuntimeArray*)L_29)->max_length)))))
		{
			goto IL_0055;
		}
	}
	{
		goto IL_0097;
	}

IL_0074:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_30 = V_0;
		V_2 = L_30;
		V_3 = 0;
		goto IL_0091;
	}

IL_007a:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_31 = V_2;
		int32_t L_32 = V_3;
		NullCheck(L_31);
		int32_t L_33 = L_32;
		String_t* L_34 = (L_31)->GetAt(static_cast<il2cpp_array_size_t>(L_33));
		V_6 = L_34;
		StringBuilder_t* L_35 = V_1;
		String_t* L_36 = V_6;
		NullCheck(L_35);
		StringBuilder_t* L_37;
		L_37 = StringBuilder_AppendFormat_mFA88863E4018C2912D1A783E0EA6DAE4F594124F(L_35, _stringLiteralB2A649209AE1998E0B4A2FCC12F79B9D1FB88DC1, L_36, NULL);
		int32_t L_38 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_38, 1));
	}

IL_0091:
	{
		int32_t L_39 = V_3;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_40 = V_2;
		NullCheck(L_40);
		if ((((int32_t)L_39) < ((int32_t)((int32_t)(((RuntimeArray*)L_40)->max_length)))))
		{
			goto IL_007a;
		}
	}

IL_0097:
	{
		StringBuilder_t* L_41 = V_1;
		NullCheck(L_41);
		String_t* L_42;
		L_42 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_41);
		return L_42;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		il2cpp_codegen_runtime_class_init_inline(il2cpp_rgctx_data(method->rgctx_data, 2));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ((EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 2)))->___Value;
		return L_0;
	}
}
