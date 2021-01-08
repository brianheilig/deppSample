// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the DEPPEXPORT_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// DEPPEXPORT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DEPPEXPORT_EXPORTS
#define DEPPEXPORT_API __declspec(dllexport)
#else
#define DEPPEXPORT_API __declspec(dllimport)
#endif

// This class is exported from the dll
class DEPPEXPORT_API CDeppExport {
public:
	CDeppExport(void);
	// TODO: add your methods here.
};

extern DEPPEXPORT_API int nDeppExport;

DEPPEXPORT_API int fnDeppExport(void);
