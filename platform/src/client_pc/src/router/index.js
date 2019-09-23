// 引入vue和router组件
import Vue from 'vue'
import Router from 'vue-router'
import spLogin from '../components/spLogin'
import spRegister from '../components/spRegister'

// 三方组件需要use
Vue.use(Router)

// 路由配置
export default new Router({
  routes: [{
    path: '/login',
    component: spLogin
  }, {
    path: '/register',
    component: spRegister
  }]
})
