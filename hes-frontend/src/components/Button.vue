<template>
    <button @click="$router.push(link)" :style="{background: color, width: width}" class="btn">{{text}}</button>
</template>

<script>
export default {
    name: 'Button',
    props: {
        text: String,
        color: String,
        width: String,
        link: String
    }, 
    methods: {
    }
}
</script>

<style>
.btn {
    display: inline-block;
    background: #000;
    color: #fff;
    border: none;
    padding: 10px 20px;
    margin: 5px;
    border-radius: 5px;
    cursor: pointer;
    text-decoration: none;
    font-size: 15px;
    font-family: inherit;
  }
  .btn:hover {
    opacity: 0.85;
    border: 1px solid #000000
  }
  .btn:focus {
    outline: none;
  }
  .btn:active {
    transform: scale(0.98);
  }
  .btn-block {
    display: block;
    width: 100%;
  }
</style>