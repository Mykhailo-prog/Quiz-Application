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
    async load() {
      await this.loadUsers();
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
  async mounted() {
    await this.load();
    localStorage.removeItem("token");
  },
  components: {
    UserList,
    LoginUser,
  },
};
</script>
<style scoped src="./Main.css"></style>
