import React, { useEffect, useState } from 'react';
import LogDisplay from './components/LogDisplay';
import { Container, Typography } from '@mui/material';

const App: React.FC = () => {
    const [logs, setLogs] = useState<string[]>([]);

    useEffect(() => {
        const eventSource = new EventSource('http://11.0.0.1:5020/api/journal/stream');
    
        eventSource.onmessage = (event) => {
            setLogs((prevEntries) => [...prevEntries, event.data]);
        };
    
        eventSource.onerror = (error) => {
          console.error('EventSource failed:', error);
          eventSource.close();
        };
    
        return () => {
          eventSource.close();
        };
      }, []);

      const downloadLogs = () => {
        const link = document.createElement('a');
        link.href = 'https://chrisalaxelrto.porebazu.lat/api/journal/download';
        link.download = 'logs.txt';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };

    return (
        <Container>
            <Typography variant="h4" gutterBottom sx={{ marginTop: 2, width: '80%', maxWidth: '1000px' }}>
                Ukulele
            </Typography>
            <LogDisplay logs={logs} downloadLogs={downloadLogs} />
        </Container>
    );
};

export default App;