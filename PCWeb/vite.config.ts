import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import AutoImport from 'unplugin-auto-import/vite'; //按需导入
import Components from 'unplugin-vue-components/vite';
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers';
import { resolve } from 'path'
import viteCompression from 'vite-plugin-compression';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    AutoImport({
      resolvers: [ElementPlusResolver()],
    }),
    Components({
      resolvers: [ElementPlusResolver()],
    }),
    viteCompression({
      algorithm: 'gzip',
      deleteOriginFile: false, // 不删除源文件
    }),
  ],
  server: {
    port: 3000,
    open: true,
    proxy: {
      '/api': {
        target: 'http://localhost:7016/api',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
      '/ChatHubApi': {
        target: 'http://127.0.0.1:7016',
        ws: true,  //代理websockets
        changeOrigin: true, // 虚拟的站点需要更管origin
      }
    },
  },
  build: {
    target: ['edge90', 'chrome90', 'firefox90', 'safari15'],
  },
  resolve: {
    // 设置路径别名
    alias: {
      '@': resolve(__dirname, './src'),
      '*': resolve('')
    },
  }
});
