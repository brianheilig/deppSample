# A makefile for the Digilent Adept Asynchronous Parallel Port interface (DEPP)

# This makefile links the 64-bit depp.lib which was downloaded from
# https://reference.digilentinc.com/reference/software/adept/start?redirect=1#software_downloads
# into a dll suitable for use in a C# application using PInvoke

MODULE_DEF=deppExport.def
LIBS=depp.lib libvcruntime.lib
LIBDIR=.\lib\digilent.adept.sdk_v2.4.2\lib

all: depp.dll

clean:
	del /f depp.lib depp.exp depp.dll

depp.dll:
	link.exe /dll /machine:x86 /def:$(MODULE_DEF) /out:depp.dll /libpath:$(LIBDIR) $(LIBS)
