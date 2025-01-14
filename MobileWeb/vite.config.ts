import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
//在vite.config.ts中配置
import AutoImport from "unplugin-auto-import/vite"
import Components from 'unplugin-vue-components/vite';
import { VantResolver } from '@vant/auto-import-resolver';
import { resolve } from 'path'
import viteCompression from 'vite-plugin-compression';

 
export default defineConfig({
  plugins: [
    vue(),
    AutoImport({
      imports: [ 'vue', 'vue-router'] //自动导入相关函数
    }),
    Components({
      resolvers: [VantResolver()],
    }),
    viteCompression({
      algorithm: 'gzip',
      deleteOriginFile: false, // 不删除源文件
    }),
  ],
  server: {
    port: 3001,
    open: true,
    proxy: {
      '/api': {
        target: 'http://localhost:7016/api',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
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
})