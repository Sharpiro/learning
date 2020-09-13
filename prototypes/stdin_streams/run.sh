cargo build
# echo 123456789a | ./target/debug/stdin_streams
socat TCP4-Listen:8080,reuseaddr,fork EXEC:./target/debug/stdin_streams
# socat -v TCP4-Listen:8080,reuseaddr TCP:localhost:8081
