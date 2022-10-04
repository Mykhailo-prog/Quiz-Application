<template src="./Pugination.html"></template>
<script>
import { mapActions, mapState, mapGetters, mapMutations } from "vuex";
export default {
  props: {},
  methods: {
    ...mapActions([
      "changeCounter",
      "finishTest",
      "setEndTime",
      "loadUsers",
      "updCurrUser",
      "cleanAnswers",
      "cleanCounter",
    ]),
    QuestPos(quest) {
      return this.Questions.indexOf(quest);
    },
    async Finish() {
      this.setEndTime();
      await this.finishTest();
      await this.loadUsers();
      this.cleanCounter();
      this.cleanAnswers();
      this.updCurrUser(this.CurrentUser);
      this.$router.push("/result");
    },
  },
  computed: {
    ...mapGetters([
      "Questions",
      "PuginationCounter",
      "CheckFinish",
      "Answers",
      "CurrentUser",
    ]),
  },
};
</script>
<style scoped src="./Pugination.css"></style>
