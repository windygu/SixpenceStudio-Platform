import csSelect from './select'
import csInput from './input'
import login from './login'

const components = [
  csSelect,
  csInput,
  login
]

const install = function (Vue) {
  if (install.installed) return
  components.map(component => Vue.component(component.name, component))
}

export default {
  csSelect,
  csInput,
  login
}
