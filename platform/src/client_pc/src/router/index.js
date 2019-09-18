import Vue from 'vue'
import Router from 'vue-router'
import csSelect from '../components/select'
import csInput from '../components/input'
import login from '../components/login'

Vue.use(Router)

export default new Router({
  routes: [{
    path: '/select',
    component: csSelect
  }, {
    path: '/input',
    component: csInput
  }, {
    path: '/login',
    component: login
  }]
})
