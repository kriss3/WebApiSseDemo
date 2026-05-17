
import { useEffect, useRef, useState } from 'react';
import { startSyncStream, type SyncEvent } from '../services/syncService';

type LogEntry = { id: number; text: string; done?: boolean };

export const SyncProgressList = () => {

	const [log, setLog] = useState<LogEntry[]>([]);
	const [running, setRunning] = useState(false);
	const cancelRef = useRef<(() => void) | null>(null);
	const nextId = useRef(0);

	const append = (text: string, done = false) =>
		setLog((l) => [...l, { id: nextId.current++, text, done }]);

    const handleEvent = (e: SyncEvent) => {
        if (e.kind === 'progress') {
            append(`${e.data.current}/${e.data.total} — ${e.data.message}`);
        } else if (e.kind === 'complete') {
            append(`✓ ${e.data.message}`, true);
            setRunning(false);
        } else {
            append(e.message);
            setRunning(false);
        }
    };

    const start = () => {
        setLog([]);
        setRunning(true);
        cancelRef.current = startSyncStream(handleEvent);
    };

    const stop = () => {
        cancelRef.current?.();
        setRunning(false);
    };

    // If the component unmounts mid-stream, close the connection.
    useEffect(() => {
        return () => cancelRef.current?.();
    }, []);

    return (
        <div>
            <button onClick={running ? stop : start}>
                {running ? 'Stop' : 'Start sync'}
            </button>
            <ul>
                {log.map((l) => (
                    <li key={l.id} style={{ fontWeight: l.done ? 'bold' : 'normal' }}>
                        {l.text}
                    </li>
                ))}
            </ul>
        </div>
    );
};