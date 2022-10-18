<template src="./AdminPage.html"> </template>
<script>
import UserList from "@/components/UserList";
import { mapGetters } from "vuex";
export default {
  components: {
    UserList,
  },
  computed: {
    ...mapGetters(["Users", "Tests"]),
  },
};
</script>
<style scoped src="./AdminPage.css"></style>
