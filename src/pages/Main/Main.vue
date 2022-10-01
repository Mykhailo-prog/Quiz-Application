<template src="./Main.html"></template>
<script>
import UserList from "@/components/UserList";
import LoginUser from "@/components/LoginUser";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Main",
  data() {
    return {};
  },

  methods: {
    ...mapActions(["loadUsers"]),
    load() {
      this.loadUsers();
    },
    checkStatus() {
      if (this.GetUsers != []) {
        this.userListStatus = true;
      } else {
        this.userListStatus = false;
      }
    },
  },
  computed: {
    ...mapGetters(["Users"]),
  },
  mounted() {
    this.load();
    localStorage.removeItem("token");
  },
  components: {
    UserList,
    LoginUser,
  },
};
</script>
<style scoped src="./Main.css"></style>
