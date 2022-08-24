<template src="./QuestInput.html"></template>
<script>
import AnswerInput from "@/components/AnswerInput";
export default {
  props: {
    counter: {
      type: Number,
    },
  },
  data() {
    return {
      QuestVisible: false,
      AnsCounter: 1,
    };
  },
  components: {
    AnswerInput,
  },
};
</script>
<style scoped src="./QuestInput.css"></style>
