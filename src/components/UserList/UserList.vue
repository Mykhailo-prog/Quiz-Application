<template src="./UserList.html"></template>
<script>
import { mapGetters, mapActions } from "vuex";
export default {
  props: {
    users: {
      type: Array,
      required: true,
    },
    tests: {
      type: Array,
    },
  },
  data() {
    return {
      pass: null,
    };
  },
  methods: {
    ...mapActions([
      "loadUsers",
      "deleteUser",
      "adminConfirmEmail",
      "resetScore",
      "changePassword",
      "delTest",
    ]),
    async DelUser(Name) {
      await this.deleteUser({ name: Name });
      await this.loadUsers();
    },
    async ConfirmEmail(Name) {
      await this.adminConfirmEmail({ name: Name });
      await this.loadUsers();
    },
    async ResetScore(Name) {
      await this.resetScore({ name: Name });
      await this.loadUsers();
    },
    async ResetPassword(Name) {
      await this.changePassword({ name: Name, password: this.pass });
      this.pass = null;
      await this.loadUsers();
    },
    async DeleteTest(id) {
      await this.delTest(id);
    },
  },
  computed: {
    ...mapGetters(["CurrentUser"]),
  },
};
</script>
<style scoped src="./UserList.css"></style>
