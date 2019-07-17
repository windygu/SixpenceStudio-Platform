import csSelect from './select'
import csInput from './input'

const components = [
  csSelect,
  csInput
]

const install = function (Vue) {
  if (install.installed) return
  components.map(component => Vue.component(component.name, component))
}

export default {
  install,
  csSelect,
  csInput
}
