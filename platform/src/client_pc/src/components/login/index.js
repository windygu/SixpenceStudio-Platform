import login from './login.vue'

login.install = function (Vue) {
  Vue.component(login.name, login)
}

export default login
