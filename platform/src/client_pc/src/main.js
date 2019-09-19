// 引入 vue 和 app
import Vue from 'vue'
import App from './App'
// 引入路由
import router from './router'
// 引入 element-ui 组件库
import ElementUI from 'element-ui'
// 引入 element-ui css
import 'element-ui/lib/theme-chalk/index.css'

// 第三方组件需要use
Vue.use(ElementUI)

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
