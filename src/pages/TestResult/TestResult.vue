<template src="./TestResult.html"></template>
<script>
import { mapActions, mapState, mapGetters, mapMutations } from "vuex";
export default {
  name: "TestResult",
  data() {
    return {
      End: null,
    };
  },
  methods: {
    ...mapActions({
      Finish: "finishTest",
      CleanUsers: "cleanUsers",
      CleanCounter: "cleanCounter",
      CleanAnswers: "cleanAnswers",
      Load: "loadUsers",
    }),
    ...mapMutations(["SET_CURRENT_USER"]),
    backToTests() {
      this.$router.push("/tests");
      this.CleanAnswers();
      this.CleanCounter();
    },
    exit() {
      this.$router.push("/");
      this.CleanUsers();
      this.CleanCounter();
      this.CleanAnswers();
    },
    setTime() {
      this.End = [new Date().getMinutes(), new Date().getSeconds()];
    },
    async finishTest() {
      this.setTime();
      await this.Finish({
        user: this.currUser,
        testId: this.currTestId,
        time: this.userTime(this.Start, this.End),
      });
      await this.Load();
      this.SET_CURRENT_USER(this.currUser);
    },
    userTime(start, end) {
      return `${
        end[1] > start[1] ? end[0] - start[0] : end[0] - start[0] - 1
      }:${
        end[1] > start[1] ? end[1] - start[1] : Math.abs(start[1] - 60) + end[1]
      }`;
    },
  },
  computed: {
    ...mapState({
      test: (state) => state.tests.currTest,
      result: (state) => state.answers.procResult,
      Start: (state) => state.tests.start,
    }),
    ...mapGetters(["currUser", "currTestId"]),
  },
  beforeMount() {
    //this.finishTest();
  },
  components: {},
};
</script>
<style scoped src="./TestResult.css"></style>
