export type SyncProgress = {
	current: number;
	total: number;
	message: string;
};

export type SyncEvent =
	| { kind: 'progress'; data: SyncProgress }
	| { kind: 'complete'; data: SyncProgress }
	| { kind: 'error'; message: string };

const API_BASE = 'https://localhost:7055';

export function startSyncStream(onEvent: (e: SyncEvent) => void): () => void {

	const es = new EventSource(`${API_BASE}/api/sync/stream`);

	es.addEventListener('progress', (ev) =>
		onEvent({ kind: 'progress', data: JSON.parse(ev.data) })
	);

	es.addEventListener('complete', (ev) => {
		onEvent({ kind: 'complete', data: JSON.parse(ev.data) });
		es.close();
	});

	es.onerror = () => onEvent({ kind: 'error', message: 'Stream closed' });

	return () => es.close();

};