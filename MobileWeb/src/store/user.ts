import { MenuModel } from "@/class/MenuModel";
import { generateRouter, setAddRoute } from "@/router/dynicRoute";
import { defineStore } from "pinia";

export const userStore = defineStore("order", {
  //存放需要共享的数据
  state: () => {
    return {
      UserId: 0,
      Token: localStorage['token'],
      NickName: localStorage['nickname'],
      permissionList: [] as MenuModel[],
    }
  },
  // //Store 状态的计算值
  // getters: {
  //   doubleCount: (state) => state.count * 2
  // },
  //存放同步和异步方法
  actions: {
    EditUserId(id: number) {
      this.UserId = id;
    },
    SettingNickName(NickName: string) {
      this.NickName = NickName;
    },
    SettingToken(Token: string) {
      this.Token = Token;
    },
    async SettingMenu(result: MenuModel[]) {
      this.permissionList = result
    },
    addRouter() {
      // 生成路由树
      // routerList在登陆成功时获取到，在跳转页面之前存起来
      const routerList = generateRouter(this.permissionList)
      // 添加路由
      setAddRoute(routerList)
    },
  },
  //开启数据持久化存储
  persist: {
    enabled: true,
    strategies: [
      {
        //这个名称再浏览器本地存储的name
        key: 'site',
        storage: localStorage,
      },
    ],
  }
})