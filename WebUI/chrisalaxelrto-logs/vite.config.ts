import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    host: '0.0.0.0', // Allow access from external hosts
    port: 5173, // Default Vite port
    allowedHosts: ['chrisalaxelrto.porebazu.lat'],
    hmr: {
      host: 'chrisalaxelrto.porebazu.lat',
      port: 5173,
    },
  },
});