# A makefile for the Digilent Adept Asynchronous Parallel Port interface (DEPP)

# This makefile links depp.lib and dmgr.lib which were downloaded from
# https://reference.digilentinc.com/reference/software/adept/start?redirect=1#software_downloads
# into a dll suitable for use in a C# application using PInvoke

# Download the Adept SDK from the above link and place it into the lib directory

MODULE_DEF=depp.def
ADEPT_LIB=digilent.adept.sdk_v2.4.2
!IF "$(PROCESSOR_ARCHITECTURE)" == "x86"
LIBDIR=lib\$(ADEPT_LIB)\lib
MACHINE=x86
!ELSE
LIBDIR=lib\$(ADEPT_LIB)\lib64
MACHINE=x64
!ENDIF
LIBS=depp.lib

all: $(MACHINE)\depp.dll

"$(MACHINE)\emptyDllMain.obj": emptyDllMain.c
	cl /Fo$@ -c $**

"$(MACHINE)\depp.dll": "$(MACHINE)\emptyDllMain.obj"
	link.exe /dll /machine:$(MACHINE) /def:$(MODULE_DEF) /out:$@ /entry:DllMain /libpath:$(LIBDIR) $** $(LIBS)

