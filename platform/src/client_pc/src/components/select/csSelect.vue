<template>
  <el-select v-bind="$props"
             placeholder="请选择"></el-select>
</template>

<script>
import Select from 'element-ui'

export default {
  name: 'cs-select',
  props: {
    ...Select.props
  }
}
</script>

<style>
</style>
