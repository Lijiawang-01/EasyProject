//在store/index.ts配置pinia和共享数据持久化处理

import { createPinia } from "pinia";
import piniaPluginPersist from "pinia-plugin-persist"

const store = createPinia()
store.use(piniaPluginPersist)

export default store
