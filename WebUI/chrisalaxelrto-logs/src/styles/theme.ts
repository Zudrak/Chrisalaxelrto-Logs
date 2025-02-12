import { createTheme } from '@mui/material/styles';
import { cyan } from '@mui/material/colors';

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: cyan[500],
    },
    secondary: {
      main: cyan[700],
    },
    background: {
      default: '#121212',
      paper: '#1d1d1d',
    },
    text: {
      primary: '#ffffff',
      secondary: '#b0bec5',
    },
  },
});

export default theme;