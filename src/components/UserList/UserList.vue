<template src="./UserList.html"></template>
<script>
export default {
  props: {
    users: {
      type: Array,
      required: true,
    },
  },
};
</script>
<style scoped src="./UserList.css"></style>
