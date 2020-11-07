<template>
  <div>
    <a-menu v-model="menu" mode="horizontal">
      <a-menu-item key="keyWords">关键词回复</a-menu-item>
      <a-menu-item key="forcus">关注回复</a-menu-item>
    </a-menu>
  </div>
</template>

<script>
export default {
  name: 'autoReply',
  data() {
    return {
      menu: ['keyWords']
    };
  }
};
</script>

<style></style>
