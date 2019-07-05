<template>
  <el-select v-bind="$props" @change="changeEmit" @visible-change='visibleChangeEmit'>
    <el-option
      v-for="item in options"
      :key="item[propsAlias.value]"
      :label="item[propsAlias.label]"
      :value="item[propsAlias.label] + '·' + item[propsAlias.value]">
    </el-option>
  </el-select>
</template>

<script>
import select from  'element-ui'

export default {
  name: 'cs-select',
  props: {
    ...Select.props,
    options: {
      type: Array,
      default: () => [
        {
          value: '选项1',
          label: '蛋糕'
        },
        {
          value: '选项2',
          label: '可乐'
        }
      ]
    },
    propsAlias: {
      type: object,
      default: () => {
        return {
          value: 'value',
          label: 'label'
        }
      }
    }
  },
  data() {
    return {

    }
  },
  methods: {
    changeEmit(val){
      this.$emit('change', val);    // 选中值发生变化时触发
    },
    visibleChangeEmit(isVisible){
      this.$emit('visible-change', isVisible);  // 下拉框出现/隐藏时触发
    }
  }
}
</script>

<style>

</style>
