import { createI18n } from 'vue-i18n';
import messages from './index';
let currentLanguage = navigator.language.replace(/-(\S*)/, '')
// 如果本地缓存记录了语言环境，则使用本地缓存
const lsLocale = localStorage.getItem('lang') || 'cn'
if (lsLocale) {
  currentLanguage = lsLocale
}
const i18n = createI18n({
  globalInjection: true,
  legacy: false, // you must specify 'legacy: false' option
  locale: currentLanguage, //language.split("-")[0] || "zh",
  messages,
});
export const langs = [
  { key: 'zh', title: '中文' },
  { key: 'en', title: 'English' }
]
export default i18n;
