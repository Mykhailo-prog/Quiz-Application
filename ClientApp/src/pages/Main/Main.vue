<template src="./Main.html"></template>
<script>
import UserList from "@/components/UserList";
import LoginUser from "@/components/LoginUser";
import RegisterUser from "@/components/RegisterUser";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Main",
  data() {
    return {
      navItem: 1,
    };
  },

  methods: {
    ...mapActions(["loadUsers", "cleanUsers"]),
    confirmAlert() {
      this.$bvModal.show("confirmAlert");
      this.navItem--;
    },
  },
  computed: {
    ...mapGetters(["Users"]),
  },
  async created() {
    this.cleanUsers();
    await this.loadUsers();
    localStorage.removeItem("token");
  },
  components: {
    UserList,
    LoginUser,
    RegisterUser,
  },
};
</script>
<style scoped src="./Main.css"></style>
