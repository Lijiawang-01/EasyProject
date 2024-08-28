module.exports = {
  env: {
    browser: true,
    es2021: true,
    node: true,
    jest: true,
  },
  /* 指定如何解析语法 */
  parser: "vue-eslint-parser",
  parserOptions: {
    ecmaVersion: "latest",
    parser: "@typescript-eslint/parser",
    sourceType: "module",
  },
  extends: [
    "eslint:recommended",
    "plugin:@typescript-eslint/recommended",
    "plugin:vue/vue3-essential",
  ],
  overrides: [
    {
      env: {
        node: true,
      },
      files: [".eslintrc.{js,cjs}"],
      parserOptions: {
        sourceType: "script",
      },
    },
  ],
  plugins: ["@typescript-eslint", "vue"],
  rules: {
    // 参考 https://typescript-eslint.io/
    // 禁止// @ts-ignore
    "@typescript-eslint/ban-ts-ignore": "off",
    //要求函数和类方法有显式返回类型。
    "@typescript-eslint/explicit-function-return-type": "off",
    //禁用any类型
    "@typescript-eslint/no-explicit-any": "off",
    //除 import 语句外，不允许使用 require 语句。
    "@typescript-eslint/no-var-requires": "off",
    //禁止空函数
    "@typescript-eslint/no-empty-function": "off",
    //在定义变量之前禁止使用变量。
    "@typescript-eslint/no-use-before-define": "off",
    //禁止 @ts-<directive> 注释或要求指令后有描述。
    "@typescript-eslint/ban-ts-comment": "off",
    //禁止某些类型。
    "@typescript-eslint/ban-types": "off",
    //禁止使用 ! 进行非空断言后缀运算符。
    "@typescript-eslint/no-non-null-assertion": "off",
    //要求导出函数和类的公共类方法显式返回和参数类型。
    "@typescript-eslint/explicit-module-boundary-types": "off",
    // 参考 https://eslint.vuejs.org/rules/
    //强制执行自定义事件名称的特定大小写
    "vue/custom-event-name-casing": "off",
    //强制执行属性顺序
    "vue/attributes-order": "off",
    //强制每个组件都应位于自己的文件中
    "vue/one-component-per-file": "off",
    //不允许在标签的右括号之前换行
    "vue/html-closing-bracket-newline": "off",
    //强制每行的最大属性数
    "vue/max-attributes-per-line": "off",
    //需要在多行元素的内容之前和之后换行
    "vue/multiline-html-element-content-newline": "off",
    //需要在单行元素的内容之前和之后换行
    "vue/singleline-html-element-content-newline": "off",
    //对模板中的自定义组件强制执行属性命名样式
    "vue/attribute-hyphenation": "off",
    //强制执行自关闭风格
    "vue/html-self-closing": "off",
    //禁止向模板添加多个根节点
    "vue/no-multiple-template-root": "off",
    "vue/require-default-prop": "off",
    //禁止向自定义组件中使用的 v-model 添加参数
    "vue/no-v-model-argument": "off",
    //禁止使用箭头函数来定义观察者
    "vue/no-arrow-functions-in-watch": "off",
    //禁止 <template> 上的key属性
    "vue/no-template-key": "off",
    //禁止使用v-html以防止XSS攻击
    "vue/no-v-html": "off",
    //支持 <template> 中的注释指令
    "vue/comment-directive": "off",
    //禁止 <template> 中出现解析错误
    "vue/no-parsing-error": "off",
    //禁止使用已弃用的 .native 修饰符（在 Vue.js 3.0.0+ 中）
    "vue/no-deprecated-v-on-native-modifier": "off",
    //要求组件名称始终为多个单词
    "vue/multi-word-component-names": "off",
    // 参考 http://eslint.cn/docs/rules/
    //禁止添加论据v-model 用于定制组件
    "no-v-model-argument": "off",
    //禁止使用不必要的转义字符
    "no-useless-escape": "off",
    //禁止稀疏数组
    "no-sparse-arrays": "off",
    //禁止直接在对象上调用某些 Object.prototype 方法
    "no-prototype-builtins": "off",
    //禁止条件中的常量表达式
    "no-constant-condition": "off",
    //在定义变量之前禁止使用变量
    "no-use-before-define": "off",
    //禁止指定的全局变量
    "no-restricted-globals": "off",
    //不允许指定的语法
    "no-restricted-syntax": "off",
    //在生成器函数中围绕*运算符强制执行一致的间距
    "generator-star-spacing": "off",
    //不允许在return、throw、continue和break语句之后出现无法访问的代码
    "no-unreachable": "off",
    //vue2只有一个节点但是vue3支持多个
    "no-multiple-template-root": "off",
    //该规则旨在消除未使用的变量，函数和函数的参数。
    "no-unused-vars": "off",
    "@typescript-eslint/no-unused-vars": ["off"],
    //禁止case声明
    "no-case-declarations": "off",
    //禁止console
    "no-console": "warn",
    //禁止console
    "no-debugger": "warn",
  },
};