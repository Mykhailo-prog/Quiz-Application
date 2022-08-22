<template src="./Pugination.html"></template>
<script>
import { mapActions, mapState, mapGetters } from "vuex";
export default {
  methods: {
    ...mapActions({
      Inc: "increaseCounter",
      Dec: "decreaseCounter",
      Set: "getCounter",
      Finish: "finishTest",
      Load: "loadUsers",
      Test: "test",
      CleanUsers: "cleanUsers",
      CleanCounter: "cleanCounter",
      CleanAnswers: "cleanAnswers",
    }),
    ...mapGetters(["getAnsLen"]),

    Prev() {
      this.Dec();
    },
    Next() {
      this.Inc();
    },
    setCount(count) {
      this.Set(count);
    },
    async finishTest() {
      await this.Finish({ user: this.CurrUser, testId: this.TestId });
      this.$router.push("/");
      await this.CleanUsers();
      await this.CleanCounter();
      await this.CleanAnswers();
      this.Test();
    },
  },
  computed: {
    ...mapState({
      quests: (state) => state.questions.questList,
      answers: (state) => state.answers.answerList,
      checkParam: (state) => state.answers.checkFinish,
      counter: (state) => state.questions.pugCounter,
    }),
    ...mapGetters(["currUser", "currTestId"]),
    CurrUser() {
      return this.currUser;
    },
    TestId() {
      return this.currTestId;
    },
  },
};
</script>
<style scoped src="./Pugination.css"></style>
