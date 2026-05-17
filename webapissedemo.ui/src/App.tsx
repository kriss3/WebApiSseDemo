import { useState } from 'react'
import reactLogo from './assets/react.svg'

import { SyncProgressList } from './components/SyncProgressList';


import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
import './App.css'

export default function App() {
    return (
        <main style={{ fontFamily: 'system-ui', padding: '1rem', maxWidth: 600 }}>
            <h1>Plant sync</h1>
            <SyncProgressList />
        </main>
    );
}
