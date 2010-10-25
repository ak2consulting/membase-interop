option explicit

dim res
dim nscf, nsc

set nscf = CreateObject("Membase.Interop.MembaseClientFactory")
set nsc = nscf.Create("D:\northscale-com\Sample\nsc_localbuild.config")

res = nsc.SetWithExpiration("foo", "bar", CDate("2011-08-20 22:00"))
wscript.echo res

res = nsc.Get ("foo")

wscript.echo res