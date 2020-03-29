<template>
  <div class="header">
    <slot />
  </div>
</template>

<script>
export default {
  name: 'spHeader',
  data() {
    return {};
  }
};
</script>

<style lang="less" scoped>
.header {
  line-height: 60px;
  height: 60px;
}
</style>
