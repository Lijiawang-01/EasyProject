import { defineStore } from 'pinia';
import { TagModel } from '@/class/TagModel';
import { generateRouter, setAddRoute } from '@/router/dynicRoute'
import { MenuModel } from '@/views/admin/menu/class/MenuModel';
import i18n from '@/local/i18n'

const store = defineStore('counterStore', {
  state: () => ({
    UserId: '',
    TagArrs: [] as TagModel[],
    Token: localStorage['token'],
    NickName: localStorage['nickname'],
    permissionList: [] as MenuModel[],
    Local: i18n.global.locale.value
  }),
  actions: {
    EditUserId(id: string) {
      this.UserId = id;
    },
    AddTag(tag: TagModel) {
      if ((this.TagArrs as TagModel[]).filter((item) => item.index.indexOf(tag.index) > -1).length == 0) {
        this.TagArrs.push(tag);
      }
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
    // 设置locale
    SetLocale(lang: any) {
      this.Local = lang
      i18n.global.locale.value = lang
    }
  },
  persist: {
    enabled: true,
    strategies: [
      {
        //这个名称再浏览器本地存储的name
        key: 'site',
        storage: localStorage,
      },
    ],
  },
});

export default store;
