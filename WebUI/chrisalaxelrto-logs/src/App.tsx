import React, { useEffect, useState } from 'react';
import LogDisplay from './components/LogDisplay';
import { Container, Typography } from '@mui/material';

const App: React.FC = () => {
    const [logs, setLogs] = useState<string[]>([]);

    useEffect(() => {
        const eventSource = new EventSource('http://localhost:5020/api/journal/stream');
    
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


    return (
        <Container>
            <Typography variant="h4" gutterBottom sx={{ marginTop: 2 }}>
                Ukulele
            </Typography>
            <LogDisplay logs={logs} />
        </Container>
    );
};

export default App;