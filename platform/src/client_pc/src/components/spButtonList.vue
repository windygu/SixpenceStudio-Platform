<template>
  <sp-header>
    <template v-for="(button, index) in buttons">
      <el-input v-if="button.name === 'search'" v-model="searchValue" placeholder="请输入内容" @change="change" v-bind:key="index">
        <el-button slot="append" icon="el-icon-search" @click="change"></el-button>
      </el-input>
      <el-button v-else :icon="button.icon" @click="handleClick(button)" v-bind:key="index">{{ button.text }}</el-button>
    </template>
  </sp-header>
</template>

<script>
import spHeader from './spHeader';

export default {
  name: 'spButtonList',
  props: {
    // 按钮
    buttons: {
      type: Array,
      default: () => []
    }
  },
  components: { spHeader },
  data() {
    return {
      searchValue: ''
    };
  },
  methods: {
    handleClick(button) {
      if (button.operate && typeof button.operate === 'function') {
        button.operate();
      }
    },
    change() {
      this.$emit('search-change', this.searchValue);
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .el-input-group {
  width: auto;
  padding-left: 10px;
}
</style>
