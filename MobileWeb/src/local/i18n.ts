import { createI18n } from 'vue-i18n';
import en from './en';
import cn from './zh-cn';
// vant所需要引入的
import { Locale, useCurrentLang } from 'vant'
//vant中的文件夹  需要的语言和本地的语言保持一致
import enUS from 'vant/lib/locale/lang/en-US'
import zhCN from 'vant/lib/locale/lang/zh-CN'

const messages = {
  en: {
    ...enUS,
    ...en
  },
  zh: {
    ...zhCN,
    ...cn
  },
}
// 更新vant组件库本身的语言变化，支持国际化
function vantLocales(lang: string) {
  if (lang === 'en') {
    Locale.use(lang, enUS)
  } else if (lang === 'zh') {
    Locale.use(lang, zhCN)
  }
}
// 获取浏览器的语言
const lang = (navigator.language || 'en').toLocaleLowerCase()
const language = localStorage.getItem('language') || lang.split('-')[0] || 'zh';
localStorage.setItem('language', language)
// 创建i18n实例
const i18n = createI18n({
  //Not available in legacy mode 解决报错问题的配置项！！！
  legacy: false,
  // 全局注册 $t方法
  globalInjection: true,
  //设置初始化语言
  locale: language,
  // 设置备用语言
  fallbackLocale: 'en',
  messages,
})
export { i18n, vantLocales }