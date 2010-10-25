call u.bat

gacutil /i Enyim.Caching.dll
gacutil /i NorthScale.Store.dll
gacutil /i NS.Interop.dll
gacutil /i log4net.dll

regasm /unregister NS.Interop.dll
regasm /verbose NS.Interop.dll