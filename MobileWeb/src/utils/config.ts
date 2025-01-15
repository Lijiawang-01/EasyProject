declare global {
  interface Window {
    __APP_CONFIG__: {
      apiBaseUrl: string;
    };
  }
}

export function getApiUrl(): string {
  // 优先使用运行时配置
  if (window.__APP_CONFIG__?.apiBaseUrl) {
    return window.__APP_CONFIG__.apiBaseUrl;
  }
  // 回退到环境变量配置
  return import.meta.env.VITE_API_URL || '/api';
}
