<template>
  <sp-header>
    <div v-for="(button, index) in buttons" :key="index">
      <el-button :icon="button.icon" @click="handleClick(button)">{{ button.text }}</el-button>
    </div>
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
  handleClick(button) {
    if (button.operate && typeof button.operate === 'function') {
      button.operate();
    }
  }
};
</script>

<style>
</style>
