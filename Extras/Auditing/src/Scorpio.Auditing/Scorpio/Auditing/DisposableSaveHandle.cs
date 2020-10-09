using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    internal class DisposableSaveHandle : IAuditSaveHandle
    {
        private readonly AuditingManager _auditingManager;
        private readonly IDisposable _ambientScope;
        private bool _saved;
        private bool _disposedValue;

        public DisposableSaveHandle(AuditingManager auditingManager, IDisposable ambientScope, AuditInfo info, Stopwatch stopwatch)
        {
            _auditingManager = auditingManager;
            _ambientScope = ambientScope;
            Info = info;
            Stopwatch = stopwatch;
        }

        public Stopwatch Stopwatch { get; }

        public AuditInfo Info { get; }



        public void Save()
        {
            _auditingManager.Action(!_saved, m => { m.Save(this); _saved = true; });
        }

        public async Task SaveAsync()
        {
            await _auditingManager.Action(!_saved, async m => { await m.SaveAsync(this); _saved = true; });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (!_saved)
                    {
                        Save();
                    }
                    _ambientScope.Dispose();
                }


                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}