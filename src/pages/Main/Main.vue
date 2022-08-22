<template src="./Main.html"></template>
<script>
import UserList from "@/components/UserList";
import LoginUser from "@/components/LoginUser";
import { mapState, mapActions } from "vuex";

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
    loaded() {
      console.log("LOAD!!!");
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
    ...mapState({
      users: (state) => state.users.userList,
    }),
    GetUsers() {
      return this.users;
    },
  },
  mounted() {
    this.GetUsers;
    this.load();
  },
  updated() {},
  components: {
    UserList,
    LoginUser,
  },
};
</script>
<style scoped src="./Main.css"></style>
