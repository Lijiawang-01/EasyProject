import { createApp } from 'vue'
import App from './App.vue'
import "reset-css"       //样式初始化
import vant from "vant" //在main.ts中引入vant
import "../node_modules/vant/lib/index.css"
import router from './router'
import { i18n, vantLocales } from './local/i18n';
import { createPinia } from 'pinia';
import piniaPlugPersist from 'pinia-plugin-persist';
import { userStore } from '@/store/user'; //引入order模块数据
import "@/style/index.less"
//对vant组件进行初始化语言设置
vantLocales(i18n.global.locale.value)
const app = createApp(App)
app.use(createPinia().use(piniaPlugPersist));
const addRouter = () => {
  userStore().addRouter()
}
addRouter()
// router要在添加完路由之后引入，不然还没添加，路由已经生成了
app.use(router);
app.use(i18n);
app.use(vant)

app.mount('#app')