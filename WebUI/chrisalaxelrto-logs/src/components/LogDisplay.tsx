import React, { useEffect, useRef, useState } from 'react';
import { Box, Typography, Paper, IconButton, Divider, Button } from '@mui/material';
import { KeyboardDoubleArrowDown as AutoScrollIcon, CompareArrows as AutoScrollOffIcon } from '@mui/icons-material';

interface LogDisplayProps {
  logs: string[];
  downloadLogs: () => void;
}

const LogDisplay: React.FC<LogDisplayProps> = ({ logs, downloadLogs }) => {
  const boxRef = useRef<HTMLDivElement>(null);
  const [isSnapping, setIsSnapping] = useState(true);

  useEffect(() => {
    if (boxRef.current && isSnapping) {
      boxRef.current.scrollTop = boxRef.current.scrollHeight;
    }
  }, [logs, isSnapping]);

  const toggleSnapping = () => {
    setIsSnapping((prev) => !prev);
  };

  return (
    <>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Typography variant="h6" gutterBottom>
          Logs
        </Typography>
        <Box sx={{ display: 'flex', alignItems: 'center', marginBottom: "0.5em" }}>
          <Button variant="outlined" onClick={downloadLogs} sx={{ marginY: "auto" }}>
            Download
          </Button>
          <IconButton
            onClick={toggleSnapping}
            sx={{ marginY: "auto" }}
            color={isSnapping ? 'secondary' : 'default'}
          >
            {!isSnapping ? <AutoScrollOffIcon /> : <AutoScrollIcon />}
          </IconButton>
        </Box>
      </Box>
      <Divider sx={{ marginBottom: 2 }} />
      <Box
        ref={boxRef}
        sx={{ padding: 2, maxHeight: '70vh', overflowY: 'auto', backgroundColor: '#1e1e1e' }}
      >
        {logs.map((log, index) => (
          <Paper key={index} elevation={1} sx={{ marginBottom: 1, padding: 0.5, backgroundColor: '#2e2e2e' }}>
            <Typography variant="body2">{log}</Typography>
          </Paper>
        ))}
      </Box>
    </>
  );
};

export default LogDisplay;