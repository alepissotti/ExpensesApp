/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_API_URL: string;
    // Otras variables de entorno que quieras definir
  }
  
  interface ImportMeta {
    readonly env: ImportMetaEnv;
  }
  
