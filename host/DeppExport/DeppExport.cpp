// DeppExport.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "DeppExport.h"


// This is an example of an exported variable
DEPPEXPORT_API int nDeppExport=0;

// This is an example of an exported function.
DEPPEXPORT_API int fnDeppExport(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CDeppExport::CDeppExport()
{
    return;
}
