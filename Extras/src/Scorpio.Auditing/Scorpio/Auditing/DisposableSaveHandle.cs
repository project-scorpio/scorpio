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

        public DisposableSaveHandle(AuditingManager auditingManager, IDisposable ambientScope, AuditInfo info, Stopwatch stopwatch)
        {
            _auditingManager = auditingManager;
            _ambientScope = ambientScope;
            Info = info;
            Stopwatch = stopwatch;
        }

        public Stopwatch Stopwatch { get; }

        public AuditInfo Info { get; }

        public void Dispose()
        {
            if (!_saved)
            {
                Save();
            }
            _ambientScope.Dispose();
        }

        public void Save()
        {
            _saved = true;
            _auditingManager.Save(this);
        }

        public async Task SaveAsync()
        {
            _saved = true;
            await _auditingManager.SaveAsync(this);
        }
    }
}