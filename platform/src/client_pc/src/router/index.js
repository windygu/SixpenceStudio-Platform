import Vue from 'vue'
import Router from 'vue-router'
import csSelect from '../components/select'

Vue.use(Router)

export default new Router({
  routes: [{
    path: '',
    component: csSelect
  }]
})
