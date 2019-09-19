import csSelect from './select.vue'
import csInput from './input.vue'
import login from './login.vue'
import { Object } from 'core-js'

const components = {
  csSelect,
  csInput,
  login
}

export default components

// 注册组件
Object.keys(components).forEach(name => {
  Vue.component(name, components[name])
})
